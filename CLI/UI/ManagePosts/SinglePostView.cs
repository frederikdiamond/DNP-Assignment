using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;

    // Constructor that injects the post repository
    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    // Method to display a single post by its ID
    public async Task DisplayPostByIdAsync(int postId)
    {
        // Fetch the post from the repository 
        Post post = await postRepository.GetSingleAsync(postId);

        if (post == null)
        {
            Console.WriteLine($"Post with ID {postId} not found.");
            return;
        }

        // Display the post details
        Console.WriteLine("Post Details:");
        Console.WriteLine($"ID: {post.Id}");
        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");
        Console.WriteLine($"Created By User ID: {post.UserId}");
    }
}
