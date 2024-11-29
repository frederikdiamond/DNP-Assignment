using RepositoryContracts;


namespace CLI.UI.ManageUsers
{
    public class ListUsersView
    {
        private readonly IUserRepository userRepository;

        // Constructor that accepts the user repository
        public ListUsersView(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        
        public async Task DisplayUsers()
        {
            var users = await userRepository.GetManyAsync(); // Get the users from the repository

            if (!users.Any())
            {
                Console.WriteLine("No users found.");
                return;
            }

            Console.WriteLine("List of users:");
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.Id}, Username: {user.Username}");
            }
        }
    }
}