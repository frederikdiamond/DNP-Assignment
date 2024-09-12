using RepositoryContracts;
using Entities;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments;
    
     public CommentInMemoryRepository()
        {
            comments = new List<Comment>();
        }
    
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any() 
            ? comments.Max(p => p.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }
    
    public Task UpdateAsync(Comment comment)
    {
        Comment? existingPost = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{comment.Id}' not found");
        }

        comments.Remove(existingPost);
        comments.Add(comment);

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        Comment? postToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        comments.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(p => p.Id == id); 
        return Task.FromResult(comment);
    }
    
    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
}
