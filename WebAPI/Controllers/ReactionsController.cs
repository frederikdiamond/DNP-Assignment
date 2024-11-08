using ApiContracts.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

[ApiController]
[Route("[controller]")]
public class ReactionsController : ControllerBase
{
    private readonly IReactionRepository _reactionRepo;

    public ReactionsController(IReactionRepository reactionRepo)
    {
        _reactionRepo = reactionRepo;
    }

    [HttpPost]
    public async Task<ActionResult<ReactionDto>> Create([FromBody] CreateReactionDto request)
    {
        var reaction = new Reaction
        {
            UserId = request.UserId,
            PostId = request.PostId,
            CommentId = request.CommentId,
            Timestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
            UpvoteCounter = request.IsUpvote ? 1 : 0,
            DownvoteCounter = request.IsUpvote ? 0 : 1
        };

        var created = await _reactionRepo.AddAsync(reaction);
        var dto = new ReactionDto
        {
            Id = created.Id,
            UserId = created.UserId,
            PostId = created.PostId,
            CommentId = created.CommentId,
            Timestamp = created.Timestamp,
            UpvoteCounter = created.UpvoteCounter,
            DownvoteCounter = created.DownvoteCounter
        };

        return Created($"/Reactions/{dto.Id}", dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReactionDto>> GetSingle(int id)
    {
        var reaction = await _reactionRepo.GetSingleAsync(id);
        if (reaction == null)
            return NotFound();

        return Ok(new ReactionDto
        {
            Id = reaction.Id,
            UserId = reaction.UserId,
            PostId = reaction.PostId,
            CommentId = reaction.CommentId,
            Timestamp = reaction.Timestamp,
            UpvoteCounter = reaction.UpvoteCounter,
            DownvoteCounter = reaction.DownvoteCounter
        });
    }

    [HttpGet]
    public async Task<ActionResult<List<ReactionDto>>> GetMany([FromQuery] int? postId,
        [FromQuery] int? userId,
        [FromQuery] int? commentId)
    {
        var query = _reactionRepo.GetMany();
        
        if (postId.HasValue)
            query = query.Where(r => r.PostId == postId.Value);

        if (userId.HasValue)
            query = query.Where(r => r.UserId == userId.Value);

        if (commentId.HasValue)
            query = query.Where(r => r.CommentId == commentId.Value);

        var reactions = query
            .Select(r => new ReactionDto
            {
                Id = r.Id,
                UserId = r.UserId,
                PostId = r.PostId,
                CommentId = r.CommentId,
                Timestamp = r.Timestamp,
                UpvoteCounter = r.UpvoteCounter,
                DownvoteCounter = r.DownvoteCounter
            })
            .ToList();

        return Ok(reactions);
    }
    
    [HttpGet("summary/post/{postId}")]
    public ActionResult GetPostSummary(int postId)
    {
        var reactions = _reactionRepo.GetMany()
            .Where(r => r.PostId == postId)
            .ToList();

        var summary = new
        {
            TotalUpvotes = reactions.Sum(r => r.UpvoteCounter),
            TotalDownvotes = reactions.Sum(r => r.DownvoteCounter)
        };

        return Ok(summary);
    }

    [HttpGet("summary/comment/{commentId}")]
    public ActionResult GetCommentSummary(int commentId)
    {
        var reactions = _reactionRepo.GetMany()
            .Where(r => r.CommentId == commentId)
            .ToList();

        var summary = new
        {
            TotalUpvotes = reactions.Sum(r => r.UpvoteCounter),
            TotalDownvotes = reactions.Sum(r => r.DownvoteCounter)
        };

        return Ok(summary);
    }

    [HttpPut("{id}/vote")]
    public async Task<ActionResult<ReactionDto>> UpdateVote(int id, [FromQuery] bool isUpvote)
    {
        var reaction = await _reactionRepo.GetSingleAsync(id);
        if (reaction == null)
            return NotFound();
        
        reaction.UpvoteCounter = isUpvote ? 1 : 0;
        reaction.DownvoteCounter = isUpvote ? 0 : 1;
        reaction.Timestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        await _reactionRepo.UpdateAsync(reaction);

        return Ok(new ReactionDto
        {
            Id = reaction.Id,
            UserId = reaction.UserId,
            PostId = reaction.PostId,
            CommentId = reaction.CommentId,
            Timestamp = reaction.Timestamp,
            UpvoteCounter = reaction.UpvoteCounter,
            DownvoteCounter = reaction.DownvoteCounter
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _reactionRepo.DeleteAsync(id);
        return NoContent();
    }
}