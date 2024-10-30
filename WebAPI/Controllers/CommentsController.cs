using ApiContracts;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers;

public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentDto dto)
    {
        try
        {
            var created = await _commentService.CreateAsync(dto);
            return Created($"/api/comments/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments([FromQuery] string? authorUsername)
    {
        try
        {
            var comments = await _commentService.GetAsync(authorUsername);
            return Ok(comments);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
}
    
