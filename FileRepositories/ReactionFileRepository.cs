using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class ReactionFileRepository : IReactionRepository
{
    public Task<Reaction> AddAsync(Reaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Reaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Reaction> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Reaction> GetMany()
    {
        throw new NotImplementedException();
    }
}