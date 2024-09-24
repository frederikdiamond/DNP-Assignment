using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CategoryFileRepository : ICategoryRepository
{
    public Task<Category> AddAsync(Category Category)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Category> GetMany()
    {
        throw new NotImplementedException();
    }
}