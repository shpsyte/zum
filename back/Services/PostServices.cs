


public class PostService : IPostService
{
    private IPostRepository _postRepository;
    public PostService(IPostRepository repository)
    {
        this._postRepository = repository;
    }

    async public Task<IEnumerable<Post>> Get(PostFilter filter)
    {
        return await _postRepository.Get(filter);
    }
}
