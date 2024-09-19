﻿using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{      
    
    private readonly ManageUsersView manageUserView;
    private readonly ManagePostsView managePostView;
    
    public CliApp(IUserRepository userRepository, IPostRepository postRepository)
    {
        manageUserView = new ManageUsersView(userRepository);
        managePostView = new ManagePostsView(postRepository);
    }

    public async Task RunAsync()
    {
        // Main loop of the CliApp
        while (true)
        {
            Console.WriteLine("Enter 'create user', 'list users', 'create post', 'list posts', or 'exit' to quit: ");
            string action = Console.ReadLine()?.ToLower();

            switch (action)
            {
                case "create user":
                    manageUserView.CreateUserAsync();
                    break;

                case "list users":
                    manageUserView.ListUsers();
                    break;

                case "create post":
                    managePostView.CreatePostAsync();
                    break;

                case "list posts":
                    managePostView.ListPosts();
                    break;

                case "exit":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid command. Please try again.");
                    break;
            }
        }
    }

    
}