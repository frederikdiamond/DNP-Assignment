using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    
    public CreatePostView(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }
    
    public async Task DisplayCreatePostAsync()
    {
        // Logic for creating a post
        Console.Write("Enter post title: ");
        string title = Console.ReadLine();
        
        Console.Write("Enter post body: ");
        string body = Console.ReadLine();
        
        int userId;
        User user;
        
        //add the userId to the post manually,
        //will later be replaced because a proper login system will be implemented
        while (true)
        {
            Console.Write("Enter the User ID of the post's creator: ");
            
            // Check if the input can be parsed to an integer
            if (int.TryParse(Console.ReadLine(), out userId))
            {
                // Check if the user exists
                user = await userRepository.GetSingleAsync(userId);
                if (user != null)
                {
                    break; // Exit the loop if user is valid
                }
            }
            else
            {
                Console.WriteLine("Invalid User ID. Please enter a valid number.");
            }
        }

            // Create the post with valid UserId
            Post createdPost = await CreatePostAsync(title, body, user);
            Console.WriteLine($"Post '{createdPost.Title}' with body '{createdPost.Body}' created successfully by user '{user.Username}' with ID: {createdPost.PostId}!");
        }

    private async Task<Post> CreatePostAsync(string title, string body, User user)
    {
        // Create a new Post object
        /*Post newPost = new Post
        {
            Title = title,
            Body = body,
            UserId = userId,
        };*/
        
        Post newPost = new Post(title, body, user);

        // Add the post to the repository and return the created post
        return await postRepository.AddAsync(newPost);
    }
}