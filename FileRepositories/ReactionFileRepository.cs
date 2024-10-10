using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class ReactionFileRepository : IReactionRepository
{
    private readonly string filePath = "reactions.json";
    
    public ReactionFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<Reaction> AddAsync(Reaction reaction)
    {
        List<Reaction> reactions = await LoadReactionsAsync();
        int maxId = reactions.Count > 0 ? reactions.Max(r => r.Id) : 1;
        reaction.Id = maxId + 1;
        reactions.Add(reaction);
        await SaveReactionsAsync(reactions);
        return reaction;
    }

    public async Task UpdateAsync(Reaction reaction)
    {
        List<Reaction> reactions = await LoadReactionsAsync();
        int index = reactions.FindIndex(r => r.Id == reaction.Id);
        if (index != -1)
        {
            reactions[index] = reaction;
            await SaveReactionsAsync(reactions);
        }
        else
        {
            throw new KeyNotFoundException($"Reaction with ID {reaction.Id} not found.");
        }
    }

    public async Task DeleteAsync(int id)
    {
        List<Reaction> reactions = await LoadReactionsAsync();
        Reaction reactionToRemove = reactions.FirstOrDefault(r => r.Id == id);
        if (reactionToRemove != null)
        {
            reactions.Remove(reactionToRemove);
            await SaveReactionsAsync(reactions);
        }
        else
        {
            throw new KeyNotFoundException($"Reaction with ID {id} not found.");
        }
    }

    public async Task<Reaction> GetSingleAsync(int id)
    {
        List<Reaction> reactions = await LoadReactionsAsync();
        Reaction reaction = reactions.FirstOrDefault(r => r.Id == id);
        if (reaction == null)
        {
            throw new KeyNotFoundException($"Reaction with ID {id} not found.");
        }
        return reaction;
    }

    public IQueryable<Reaction> GetMany()
    {
        string reactionsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Reaction> reactions = JsonSerializer.Deserialize<List<Reaction>>(reactionsAsJson)!;
        return reactions.AsQueryable();
    }
    
    private async Task<List<Reaction>> LoadReactionsAsync()
    {
        string reactionsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Reaction>>(reactionsAsJson) ?? new List<Reaction>();
    }

    private async Task SaveReactionsAsync(List<Reaction> reactions)
    {
        string reactionsAsJson = JsonSerializer.Serialize(reactions);
        await File.WriteAllTextAsync(filePath, reactionsAsJson);
    }
}