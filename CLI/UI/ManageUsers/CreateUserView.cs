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
            
        // Call AddUser to add the new user before confirming it was created
        await AddUserAsync(username);

        Console.WriteLine($"User '{username}' created successfully!");
    }

    private async Task AddUserAsync(string username)
    {
       
        User newUser = new User
        {
            Username = username,
            // add other properties (password, etc.)
        };

        // Add the user to the repository
        await userRepository.AddAsync(newUser);
    }
    
}