using Entities;

namespace InMemoryRepositories;

public class ReactionInMemoryRepository
{
    private List<Reaction> users;
    
    
    public Task<Reaction> AddAsync(Reaction reaction)
    {
        reaction.Id = users.Any() 
            ? users.Max(p => p.Id) + 1
            : 1;
        users.Add(reaction);
        return Task.FromResult(reaction);
    }
    
    public Task UpdateAsync(Reaction reaction)
    {
        Reaction? existingPost = users.SingleOrDefault(p => p.Id == reaction.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{reaction.Id}' not found");
        }

        users.Remove(existingPost);
        users.Add(reaction);

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        Reaction? postToRemove = users.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        users.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Reaction> GetSingleAsync(int id)
    {
        Reaction? reaction = users.SingleOrDefault(p => p.Id == id);  //needs checking!! 
        return Task.FromResult(reaction);
    }
    
    
    public IQueryable<Reaction> GetMany()
    {
        return users.AsQueryable();
    }
    
    
    
    
}