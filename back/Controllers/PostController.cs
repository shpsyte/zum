using Microsoft.AspNetCore.Mvc;

namespace back.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{
    private IPostService _postService;
    public PostController(IPostRepository repository)
    {
        this._postService = new PostService(repository);
    }


    [HttpGet()]
    async public Task<IEnumerable<Post>> Get([FromQuery] PostFilter filter)
    {
        var data = await _postService.Get(filter);

        return data;
    }

}
