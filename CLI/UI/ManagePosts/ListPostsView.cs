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
    
    public async Task DisplayPostsAsync()
    {
        var posts = (await postRepository.GetManyAsync()).ToList();

        if (!posts.Any())
        {
            Console.WriteLine("No posts found.");
            return;
        }

        Console.WriteLine("List of posts:");
        foreach (var post in posts)
        {
            Console.WriteLine($"Post title: {post.Title}, body: {post.Body}, ID: {post.PostId}");
        }
    }
}