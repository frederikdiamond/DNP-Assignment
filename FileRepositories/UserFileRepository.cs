using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";
    private readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };
    
    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
        else
        {
            try
            {
                string content = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(content))
                {
                    File.WriteAllText(filePath, "[]");
                }
                else
                {
                    JsonSerializer.Deserialize<List<User>>(content, options);
                }
            }
            catch
            {
                File.WriteAllText(filePath, "[]");
            }
        }
    }
    
    public async Task<User> AddAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        
        int maxId = users.Count > 0 ? users.Max(c => c.Id) : 0;
        user.Id = maxId + 1;
        users.Add(user);
        
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

        var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            // Update user details
            existingUser.Username = user.Username; 
            
        }
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }
    

    public async Task DeleteAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

        User? userToRemove = users.FirstOrDefault(u => u.Id == id);
        if (userToRemove != null)
        {
            users.Remove(userToRemove);

            usersAsJson = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(filePath, usersAsJson);
        }
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

        return users.FirstOrDefault(u => u.Id == id);
    }

    public async Task<IQueryable<User>> GetManyAsync()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();
        return users.AsQueryable();
    }
}