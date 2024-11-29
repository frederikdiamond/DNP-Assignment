using ApiContracts.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;

    public CommentsController(
        ICommentRepository commentRepo,
        IUserRepository userRepository,
        IPostRepository postRepository)
    {
        _commentRepo = commentRepo;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create([FromBody] CreateCommentDto createCommentDto)
    {
        /*var comment = new Comment
        {
            Body = request.Body,
            PostId = request.PostId,
            UserId = request.UserId
        };*/
        
        var user = await _userRepository.GetSingleAsync(createCommentDto.UserId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var post = await _postRepository.GetSingleAsync(createCommentDto.PostId);
        if (post == null)
        {
            return NotFound("Post not found");
        }
        
        var comment = new Comment(createCommentDto.Body, user, post);
        
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
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetMany([FromQuery] int? postId, [FromQuery] int? userId)
    {
        var query = await _commentRepo.GetManyAsync();
        
        if (postId.HasValue)
            query = query.Where(c => c.PostId == postId.Value);
        
        if (userId.HasValue)
            query = query.Where(c => c.UserId == userId.Value);

        var comments = await query
            .Select(c => new CommentDto
            {
                Id = c.Id,
                Body = c.Body,
                PostId = c.PostId,
                UserId = c.UserId
            })
            .ToListAsync();

        return comments;
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
