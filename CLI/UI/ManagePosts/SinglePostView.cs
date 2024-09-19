using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    // Constructor that injects the post repository
    public SinglePostView(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    // Method to display a single post by its ID
    public async Task DisplayPostByIdAsync()
    {
        Console.Write("Enter post ID: ");
        if (int.TryParse(Console.ReadLine(), out int postId))
        {
            Post post = await postRepository.GetSingleAsync(postId);

            if (post == null)
            {
                Console.WriteLine($"Post with ID {postId} not found.");
                return;
            }
            
            User user = await userRepository.GetSingleAsync(post.UserId);
            if (user == null)
            {
                Console.WriteLine("The user who created this post was not found.");
                return;
            }

            Console.WriteLine("Post Details:");
            Console.WriteLine($"ID: {post.PostId}");
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Body: {post.Body}");
            Console.WriteLine($"Created By: {user.Username} (User ID: {user.Id})");
            Console.WriteLine($"Created At: {post.CreatedAt}");
        }
        else
        {
            Console.WriteLine("Invalid post ID.");
        }
    }
    
}
