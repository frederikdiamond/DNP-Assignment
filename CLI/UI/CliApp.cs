using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{      
    
    private readonly ManageUsersView manageUserView;
    
    public CliApp(IUserRepository userRepository)
    {
        manageUserView = new ManageUsersView(userRepository);
    }

    public void Run()
    {
        // Main loop of the CLI app
        while (true)
        {
            Console.WriteLine("Enter 'create' to create a user, 'list' to list users, or 'exit' to quit: ");
            string action = Console.ReadLine()?.ToLower();

            if (action == "create")
            {
                manageUserView.CreateUserAsync();
            }
            else if (action == "list")
            {
                manageUserView.ListUsers();
            }
            else if (action == "exit")
            {
                Console.WriteLine("Exiting...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid command. Please try again.");
            }
        }
    }
}