using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CategoryInMemoryRepository : ICategoryRepository
{
    private List<Category> categories;
    
    
    
    public Task<Category> AddAsync(Category category)
    {
        category.Id = categories.Any() 
            ? categories.Max(p => p.Id) + 1
            : 1;
        categories.Add(category);
        return Task.FromResult(category);
    }
    
    public Task UpdateAsync(Category category)
    {
        Category? existingPost = categories.SingleOrDefault(p => p.Id == category.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{category.Id}' not found");
        }

        categories.Remove(existingPost);
        categories.Add(category);

        return Task.CompletedTask;
    }
    
    
    public Task DeleteAsync(int id)
    {
        Category? postToRemove = categories.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        categores.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    
    public Task<Category> GetSingleAsync(int id)
    {
        Category? category = categories.SingleOrDefault(p => p.Id == id);  
        return Task.FromResult(category);
    }
    
    
    public IQueryable<Category> GetMany()
    {
        return categories.AsQueryable();
    }
}
