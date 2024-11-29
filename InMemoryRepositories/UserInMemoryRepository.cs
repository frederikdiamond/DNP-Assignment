using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

/*public class UserInMemoryRepository : IUserRepository
{
    private List<User> users;
    
    public UserInMemoryRepository()
    {
        users = new List<User>();
    }
    
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any() 
            ? users.Max(p => p.Id) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }
    
    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser is null)
        {
            users.Remove(existingUser);
            users.Add(user);
        }

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(p => p.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }
    
    public Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(p => p.Id == id);
        return Task.FromResult(user);
    }
    
    public async Task<IQueryable<User>> GetMany()
    {
        return users.AsQueryable();
    }
    public async Task AddUserAsync(string name, string password)
    {
        User user = new User
        {
            Username = name,
            Password = password
        };
        
        User created = await AddAsync(user);
        Console.WriteLine($"User '{created.Username}' added successfully with ID: {created.Id}");
    }
}*/
