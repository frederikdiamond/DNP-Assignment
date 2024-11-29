using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;
    
    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Comment comment)
    {
        if (!(await ctx.Comments.AnyAsync(c => c.Id == comment.Id)))
        {
            throw new NotFoundException("Comment not found");
        }
        
        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
        
        if (existing == null)
        {
            throw new NotFoundException($"Comment with id {id} not found");
        }
        
        ctx.Comments.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        var comment = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            throw new Exception($"Comment with id {id} not found");
        }
        return comment;
    }
    
    public async Task<IQueryable<Comment>> GetManyAsync()
    {
        return await Task.FromResult(ctx.Comments.AsQueryable());
    }
}
