using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    // Constructor injection for IPostRepository
    public ManagePostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public async Task CreatePostAsync()
    {
        // Create the CreatePostView and pass the repository
        CreatePostView createPostView = new CreatePostView(postRepository);
        await createPostView.DisplayCreatePostAsync();
    }

    public async Task ListPosts()
    {
        ListPostsView listPostsView = new ListPostsView(postRepository);
        await listPostsView.DisplayPostsAsync(); 
    }

    public async Task ViewPostAsync()
    {
        Console.Write("Enter post ID: ");
        if (int.TryParse(Console.ReadLine(), out int postId))
        {
            Console.WriteLine($"Attempting to retrieve post with ID: {postId}");
            
            SinglePostView singlePostView = new SinglePostView(postRepository, userRepository);
            await singlePostView.DisplayPostByIdAsync(postId); // Passing the postId directly
        }
        else
        {
            Console.WriteLine("Invalid post ID. Please enter a valid number.");
        }
    }
}