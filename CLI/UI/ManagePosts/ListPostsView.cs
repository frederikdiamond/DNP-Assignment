using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    // Constructor that accepts the user repository
    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public void DisplayPosts()
    {
        var posts = postRepository.GetMany().ToList(); // Get the posts from the repository

        if (!posts.Any())
        {
            Console.WriteLine("No posts found.");
            return;
        }

        Console.WriteLine("List of posts:");
        foreach (var post in posts)
        {
            Console.WriteLine($"Post title: {post.Title}, body: {post.Body}");
        }
    }
}