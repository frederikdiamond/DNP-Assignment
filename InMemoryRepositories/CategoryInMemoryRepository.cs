using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CategoryInMemoryRepository : ICategoryRepository
{
    private List<Category> users;
    
    
    
    public Task<Category> AddAsync(Category category)
    {
        category.Id = users.Any() 
            ? users.Max(p => p.Id) + 1
            : 1;
        users.Add(category);
        return Task.FromResult(category);
    }
    
    public Task UpdateAsync(Category category)
    {
        Category? existingPost = users.SingleOrDefault(p => p.Id == category.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{category.Id}' not found");
        }

        users.Remove(existingPost);
        users.Add(category);

        return Task.CompletedTask;
    }
    
    
    public Task DeleteAsync(int id)
    {
        Category? postToRemove = users.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        users.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    
    public Task<Category> GetSingleAsync(int id)
    {
        Category? category = users.SingleOrDefault(p => p.Id == id);  //needs checking!! 
        return Task.FromResult(category);
    }
    
    
    public IQueryable<Category> GetMany()
    {
        return users.AsQueryable();
    }
}
