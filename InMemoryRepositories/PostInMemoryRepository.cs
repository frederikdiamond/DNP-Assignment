using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    
    
    private List<Post> posts;
    
    public PostInMemoryRepository()
    {
        posts = new List<Post>();
    }
    
    public Task<Post> AddAsync(Post post)
    {
        post.PostId = posts.Any() 
            ? posts.Max(p => p.PostId) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }
    
    
    public Task UpdateAsync(Post post)
    {
        var existingPost = posts.SingleOrDefault(p => p.PostId == post.PostId);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.PostId}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        var postToRemove = posts.SingleOrDefault(p => p.PostId == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Post> GetSingleAsync(int id)
    {
        var post = posts.SingleOrDefault(p => p.PostId == id);   //needs checking!
        return Task.FromResult(post);
    }
    
    public async Task AddPostAsync(string title, string body)
    {
        Post post = new Post
        {
            Title = title,
            Body = body
        };
        
        Post created = await AddAsync(post);
        Console.WriteLine($"Post '{created.Title}' created successfully with Body: {created.Body} and Id: {created.PostId}");
    }
    
    public async Task<IQueryable<Post>> GetManyAsync()
    {
        return await Task.FromResult(posts.AsQueryable());
    }

}
