
public interface IPostService
{
    public Task<IEnumerable<Post>> Get(PostFilter filter);
}
