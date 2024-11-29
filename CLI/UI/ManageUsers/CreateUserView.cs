using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task DisplayCreateAsync()
    {
        // Logic to create a user
        Console.WriteLine("Creating a new user...");
        Console.Write("Enter username: ");
        string username = Console.ReadLine();
        
        Console.Write("Enter password: ");
        string password = Console.ReadLine();
            
        // Call AddUser to add the new user before confirming it was created
        await AddUserAsync(username, password);

        Console.WriteLine($"User '{username}' created successfully!");
    }

    private async Task AddUserAsync(string username, string password)
    {
        /*string password = null;
        User newUser = new User
        {
            Username = username,
            Password = password,
        };*/
        
        User newUser = new User(username, password);

        // Add the user to the repository
        await userRepository.AddAsync(newUser);
    }
}
