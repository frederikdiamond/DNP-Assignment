using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> GetSingleAsync(int id);
    IQueryable<User> GetMany();  
    
    
    private async Task AddUserAsync(string name, string password)
    {
        // ...
        User created = await userRepository.AddAsync(user);
        // ...
    }
}
