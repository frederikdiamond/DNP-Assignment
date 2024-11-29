using ApiContracts.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepo;
    private readonly IReactionRepository _reactionRepo;
    private readonly IUserRepository _userRepository;

    public PostsController(IPostRepository postRepo, IReactionRepository reactionRepo, IUserRepository userRepository)
    {
        _postRepo = postRepo;
        _reactionRepo = reactionRepo;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> Create([FromBody] CreatePostDto request)
    {
        /*var post = new Post
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow.ToString("o")
        };*/
        
        var user = await _userRepository.GetSingleAsync(request.UserId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var post = new Post(request.Title, request.Body, user);
        
        var created = await _postRepo.AddAsync(post);
        var dto = new PostDto
        {
            PostId = created.PostId,
            Title = created.Title,
            Body = created.Body,
            UserId = created.UserId,
            CreatedAt = created.CreatedAt
        };

        return Created($"/Posts/{dto.PostId}", dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetSingle(int id)
    {
        var post = await _postRepo.GetSingleAsync(id);
        if (post == null)
            return NotFound();

        var reactionsQuery = await _reactionRepo.GetManyAsync();
        var reactions = await reactionsQuery
            .Where(r => r.PostId == id)
            .ToListAsync();

        var dto = new PostDto
        {
            PostId = post.PostId,
            Title = post.Title,
            Body = post.Body,
            UserId = post.UserId,
            CreatedAt = post.CreatedAt,
            UpvoteCount = reactions.Sum(r => r.UpvoteCounter),
            DownvoteCount = reactions.Sum(r => r.DownvoteCounter)
        };

        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> GetMany(
        [FromQuery] string? titleContains,
        [FromQuery] int? userId,
        [FromQuery] bool orderByVotes = false,
        [FromQuery] int? top = null)
    {
        var query = await _postRepo.GetManyAsync();
        
        if (!string.IsNullOrWhiteSpace(titleContains))
        {
            query = query.Where(p => p.Title.Contains(titleContains));
        }

        if (userId.HasValue)
        {
            query = query.Where(p => p.UserId == userId.Value);
        }
        
        var posts = await query.ToListAsync();

        if (orderByVotes)
        {
            var votesQuery = await _reactionRepo.GetManyAsync();
            var postReactions = await votesQuery
                .GroupBy(r => r.PostId)
                .Select(g => new 
                { 
                    PostId = g.Key, 
                    VoteCount = g.Sum(r => r.UpvoteCounter - r.DownvoteCounter)
                })
                .ToListAsync();

            posts = posts
                .GroupJoin(
                    postReactions,
                    p => p.PostId,
                    r => r.PostId,
                    (post, reactions) => new 
                    { 
                        Post = post, 
                        VoteCount = reactions.FirstOrDefault()?.VoteCount ?? 0 
                    })
                .OrderByDescending(x => x.VoteCount)
                .Select(x => x.Post)
                .ToList();
        }

        if (top.HasValue)
        {
            posts = posts.Take(top.Value).ToList();
        }

        var reactionQuery = await _reactionRepo.GetManyAsync();
        var allReactions = await reactionQuery
            .Where(r => posts.Select(p => p.PostId).Contains(r.PostId))
            .ToListAsync();

        var dtos = posts.Select(p =>
        {
            var postReactions = allReactions.Where(r => r.PostId == p.PostId).ToList();
            return new PostDto
            {
                PostId = p.PostId,
                Title = p.Title,
                Body = p.Body,
                UserId = p.UserId,
                CreatedAt = p.CreatedAt,
                UpvoteCount = postReactions.Sum(r => r.UpvoteCounter),
                DownvoteCount = postReactions.Sum(r => r.DownvoteCounter)
            };
        }).ToList();

        return dtos;
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>> Update(int id, [FromBody] UpdatePostDto request)
    {
        var post = await _postRepo.GetSingleAsync(id);
        if (post == null)
            return NotFound();

        post.Title = request.Title;
        post.Body = request.Body;

        await _postRepo.UpdateAsync(post);

        var reactionsQuery = await _reactionRepo.GetManyAsync();
        var reactions = await reactionsQuery
            .Where(r => r.PostId == id)
            .ToListAsync();

        return Ok(new PostDto
        {
            PostId = post.PostId,
            Title = post.Title,
            Body = post.Body,
            UserId = post.UserId,
            CreatedAt = post.CreatedAt,
            UpvoteCount = reactions.Sum(r => r.UpvoteCounter),
            DownvoteCount = reactions.Sum(r => r.DownvoteCounter)
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _postRepo.DeleteAsync(id);
        return NoContent();
    }
}
