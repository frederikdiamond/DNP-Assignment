using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public async Task DisplayCreatePostAsync()
    {
        // Logic for creating a post
        Console.Write("Enter post title: ");
        string title = Console.ReadLine();
        
        Console.Write("Enter post body: ");
        string body = Console.ReadLine();
        
        await CreatePostAsync(title, body);
        Console.WriteLine($"Post '{title}' with text '{body}' created successfully!");
    }

    private async Task CreatePostAsync(string title, string body)
    {
        // Create a new Post object
        Post newPost = new Post
        {
            Title = title,
            Body = body,
        };


        // Add the post to the repository
        await postRepository.AddAsync(newPost);
    }
}