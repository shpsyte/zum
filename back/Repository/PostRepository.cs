

public class PostRepository : IPostRepository
{
    private readonly string hatchApi = "https://api.hatchways.io/assessment/blog/posts?tag={0}";

    async public Task<IEnumerable<Post>> Get(PostFilter filter)
    {
        var data = await this.GetDataFromApi(filter);
        return data;
    }


    async private Task<List<Post>> GetDataFromApi(PostFilter filter)
    {
        var posts = new List<Post>();

        var tags = filter.tags.Split(',').Select(a => a.Trim()).GroupBy(a => a).Select(a => a.Key).ToList();

        foreach (var tag in tags)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = string.Format(hatchApi, tag);
                var response = await client.GetFromJsonAsync<Root>(url);
                if (response?.Posts != null)
                {
                    posts.AddRange(response.Posts);
                }
            }
        }

        // remove duplicates
        posts = posts.GroupBy(a => a.Id).SelectMany(a => a.ToList()).ToList();

        var column = filter.sortBy ?? "id";
        var direction = filter.direction ?? "asc";

        var queryablePosts = posts.AsQueryable();
        var sortedPosts = queryablePosts.OrderByDynamic(column, direction).ToList();

        return sortedPosts;

    }

}
