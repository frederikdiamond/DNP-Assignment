using EfcRepositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;
using AppContext = EfcRepositories.AppContext;

namespace WebAPI.Controllers;

public class EfcCategoryRepository : ICategoryRepository
{
    private readonly AppContext ctx;
    
    public EfcCategoryRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Category> AddAsync(Category category)
    {
        EntityEntry<Category> entityEntry = await ctx.Categories.AddAsync(category);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }
    
    public async Task UpdateAsync(Category category)
    {
        if (!(await ctx.Categories.AnyAsync(c => c.Id == category.Id)))
        {
            throw new NotFoundException("Category not found");
        }

        ctx.Categories.Update(category);
        await ctx.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        Category? existing = await ctx.Categories.SingleOrDefaultAsync(c => c.Id == id);

        if (existing == null)
        {
            throw new NotFoundException($"Category with id {id} not found");
        }
        
        ctx.Categories.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Category> GetSingleAsync(int id)
    {
        var category = await ctx.Categories.SingleOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            throw new NotFoundException($"Category with id {id} not found");
        }

        return category;
    }

    public async Task<IQueryable<Category>> GetManyAsync()
    {
        return await Task.FromResult(ctx.Categories.AsQueryable());
    }
}
