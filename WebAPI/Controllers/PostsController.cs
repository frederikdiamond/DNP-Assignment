using ApiContracts;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers;

public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost(CreatePostDto dto)
    {
        try
        {
            var created = await _postService.CreateAsync(dto);
            return Created($"/api/posts/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts([FromQuery] string? title)
    {
        try
        {
            var posts = await _postService.GetAsync(title);
            return Ok(posts);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // Implement other CRUD methods (GetById, Update, Delete) similar to the above
}
    
}