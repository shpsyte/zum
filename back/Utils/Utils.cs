using System.Linq.Expressions;
using System.Reflection;

public class Utility<T>
{
    public List<T> GetSortedList(List<T> source, string sortColumn, SortDirection sortDirection)
    {
        var type = typeof(T);
        var sortProperty = type.GetProperty(sortColumn);

        if (sortProperty == null)
        {
            return source;
        }

        var inner = source.OrderBy(x =>
        {
            return sortProperty.GetValue(x);
        });

        if (sortDirection == SortDirection.Desc) inner.Reverse();

        return inner.ToList();

    }
}


public static class IQueryableExtensions
{
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string propertyName, string direction)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property == null) throw new ArgumentException($"Property '{propertyName}' not found.");

        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        string methodName = direction == "asc" ? "OrderBy" : "OrderByDescending";
        MethodCallExpression resultExp = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.PropertyType },
            source.Expression,
            Expression.Quote(orderByExp));

        return source.Provider.CreateQuery<T>(resultExp);
    }
}
