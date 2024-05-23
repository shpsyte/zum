
public interface IPostRepository
{
    public Task<IEnumerable<Post>> Get(PostFilter filter);
}
