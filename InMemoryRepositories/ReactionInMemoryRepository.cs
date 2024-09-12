using Entities;

namespace InMemoryRepositories;

public class ReactionInMemoryRepository
{
    private List<Reaction> reactions;
    
    
    public Task<Reaction> AddAsync(Reaction reaction)
    {
        reaction.Id = users.Any() 
            ? reactions.Max(p => p.Id) + 1
            : 1;
        reactions.Add(reaction);
        return Task.FromResult(reaction);
    }
    
    public Task UpdateAsync(Reaction reaction)
    {
        Reaction? existingPost = reactions.SingleOrDefault(p => p.Id == reaction.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{reaction.Id}' not found");
        }

        reactions.Remove(existingPost);
        reactions.Add(reaction);

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        Reaction? postToRemove = reactions.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        reactions.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Reaction> GetSingleAsync(int id)
    {
        Reaction? reaction = reactions.SingleOrDefault(p => p.Id == id);  
        return Task.FromResult(reaction);
    }
    
    
    public IQueryable<Reaction> GetMany()
    {
        return reactions.AsQueryable();
    }
    
    
    
    
}