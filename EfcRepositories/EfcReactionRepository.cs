using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcReactionRepository : IReactionRepository
{
    private readonly AppContext ctx;

    public EfcReactionRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Reaction> AddAsync(Reaction reaction)
    {
        EntityEntry<Reaction> entityEntry = await ctx.Reactions.AddAsync(reaction);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }
    
    public async Task UpdateAsync(Reaction reaction)
    {
        if (!(await ctx.Reactions.AnyAsync(r => r.Id == reaction.Id)))
        {
            throw new NotFoundException("Reaction not found");
        }

        ctx.Reactions.Update(reaction);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Reaction? existing = await ctx.Reactions.SingleOrDefaultAsync(r => r.Id == id);

        if (existing == null)
        {
            throw new NotFoundException($"Reaction with id {id} not found");
        }
        
        ctx.Reactions.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Reaction> GetSingleAsync(int id)
    {
        var reaction = await ctx.Reactions.SingleOrDefaultAsync(r => r.Id == id);
        
        if (reaction == null)
        {
            throw new NotFoundException($"Reaction with id {id} not found");
        }

        return reaction;
    }

    public async Task<IQueryable<Reaction>> GetManyAsync()
    {
        return await Task.FromResult(ctx.Reactions.AsQueryable());
    }
}
