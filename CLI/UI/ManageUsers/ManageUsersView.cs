using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository userRepository;

    // Constructor injection for IUserRepository
    public ManageUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task CreateUserAsync()
    {
        // Create the CreateUserView and pass the repository
        CreateUserView createUserView = new CreateUserView(userRepository);
        await createUserView.DisplayCreateAsync();
    }

    public void ListUsers()
    {
        ListUsersView listUsersView = new ListUsersView(userRepository);
        listUsersView.DisplayUsers();
    }
}