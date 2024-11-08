using ApiContracts.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;

    public CommentsController(ICommentRepository commentRepo)
    {
        _commentRepo = commentRepo;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create([FromBody] CreateCommentDto request)
    {
        var comment = new Comment
        {
            Body = request.Body,
            PostId = request.PostId,
            UserId = request.UserId
        };

        var created = await _commentRepo.AddAsync(comment);
        var dto = new CommentDto
        {
            Id = created.Id,
            Body = created.Body,
            PostId = created.PostId,
            UserId = created.UserId
        };

        return Created($"/Comments/{dto.Id}", dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetSingle(int id)
    {
        var comment = await _commentRepo.GetSingleAsync(id);
        if (comment == null)
            return NotFound();

        return Ok(new CommentDto
        {
            Id = comment.Id,
            Body = comment.Body,
            PostId = comment.PostId,
            UserId = comment.UserId
        });
    }

    [HttpGet]
    public ActionResult<List<CommentDto>> GetMany([FromQuery] int? postId, [FromQuery] int? userId)
    {
        var query = _commentRepo.GetMany();
        
        if (postId.HasValue)
            query = query.Where(c => c.PostId == postId.Value);
        
        if (userId.HasValue)
            query = query.Where(c => c.UserId == userId.Value);

        var comments = query
            .Select(c => new CommentDto
            {
                Id = c.Id,
                Body = c.Body,
                PostId = c.PostId,
                UserId = c.UserId
            })
            .ToList();

        return Ok(comments);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CommentDto>> Update(int id, [FromBody] UpdateCommentDto request)
    {
        var comment = await _commentRepo.GetSingleAsync(id);
        if (comment == null)
            return NotFound();

        comment.Body = request.Body;
        await _commentRepo.UpdateAsync(comment);

        return Ok(new CommentDto
        {
            Id = comment.Id,
            Body = comment.Body,
            PostId = comment.PostId,
            UserId = comment.UserId
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _commentRepo.DeleteAsync(id);
        return NoContent();
    }
}
