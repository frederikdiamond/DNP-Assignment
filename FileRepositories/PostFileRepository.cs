using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    public Task<Post> AddAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Post>> GetManyAsync()
    {
        throw new NotImplementedException();
    }
}