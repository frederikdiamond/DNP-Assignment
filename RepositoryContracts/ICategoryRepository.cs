using Entities;

namespace RepositoryContracts;

public interface ICategoryRepository 
{
    Task<Category> AddAsync(Category Category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
    Task<Category> GetSingleAsync(int id);
    Task<IQueryable<Category>> GetManyAsync();
}