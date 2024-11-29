using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext ctx;
    
    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    
    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await ctx.Posts.AnyAsync(u => u.UserId == user.Id)))
        {
            throw new NotFoundException("User not found");
        }
        
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        
        if (existing == null)
        {
            throw new NotFoundException($"User with id {id} not found");
        }

        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        var user = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        
        if (user == null)
        {
            throw new NotFoundException($"User with id {id} not found");
        }
        return user;
    }
    
    public async Task<IQueryable<User>> GetManyAsync()
    {
        return await Task.FromResult(ctx.Users.AsQueryable());
    }
}
