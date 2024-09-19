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
        
        Post createdPost = await CreatePostAsync(title, body);
        Console.WriteLine($"Post '{createdPost.Title}' with text '{createdPost.Body}' created successfully with ID: {createdPost.PostId}!");
    }

    private async Task<Post> CreatePostAsync(string title, string body)
    {
        // Create a new Post object
        Post newPost = new Post
        {
            Title = title,
            Body = body,
            // You may want to set UserId here if applicable
        };

        // Add the post to the repository and return the created post
        return await postRepository.AddAsync(newPost);
    }
}