using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";
    
    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<Post> AddAsync(Post post)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson) ?? new List<Post>();
        
        int maxId = posts.Count > 0 ? posts.Max(c => c.PostId) : 0;
        post.PostId = maxId + 1;
        posts.Add(post);
        
        postAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postAsJson);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();
        
        var existingPost = posts.FirstOrDefault(p => p.PostId == post.PostId);
        if (existingPost == null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.PostId}' not found");
        }
        
        existingPost.Title = post.Title;
        existingPost.Body = post.Body;
        
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();

        Post? postToRemove = posts.FirstOrDefault(p => p.PostId == id);
        if (postToRemove != null)
        {
            posts.Remove(postToRemove);

            postsAsJson = JsonSerializer.Serialize(posts);
            await File.WriteAllTextAsync(filePath, postsAsJson);
        }
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();

        Post post = posts.FirstOrDefault(p => p.PostId == id);
        if (post == null)
        {
            Console.WriteLine($"No post found with ID: {id}"); // Debugging
        }
        else
        {
            Console.WriteLine($"Post found: {post.Title} (ID: {post.PostId})"); // Debugging
        }

        return post;
    }

    public async Task<IQueryable<Post>> GetManyAsync()
    {
        string postsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();
        return posts.AsQueryable();
    }
}