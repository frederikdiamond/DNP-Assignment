using RepositoryContracts;
using Entities;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> users;
    
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = users.Any() 
            ? users.Max(p => p.Id) + 1
            : 1;
        users.Add(comment);
        return Task.FromResult(comment);
    }
    
    public Task UpdateAsync(Comment comment)
    {
        Comment? existingPost = users.SingleOrDefault(p => p.Id == comment.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{comment.Id}' not found");
        }

        users.Remove(existingPost);
        users.Add(comment);

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        Comment? postToRemove = users.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        users.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = users.SingleOrDefault(p => p.Id == id); 
        return Task.FromResult(comment);
    }
    
    public IQueryable<Comment> GetManyAsync()
    {
        return users.AsQueryable();
    }
    
}