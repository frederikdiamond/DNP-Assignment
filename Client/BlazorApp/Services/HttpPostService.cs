using System.Text.Json;
using ApiContracts;
using ApiContracts.DTOs;

namespace BlazorApp.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions jsonOptions;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
        this.jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<PostDto> CreateAsync(CreatePostDto request)
    {
        try
        {
            var response = await client.PostAsJsonAsync("Posts", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PostDto>(jsonOptions)
                   ?? throw new ApplicationException("Failed to deserialize post response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error creating post: {ex.Message}", ex);
        }
    }

    public async Task<PostDto> GetByIdAsync(int id)
    {
        try
        {
            var response = await client.GetAsync($"Posts/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PostDto>(jsonOptions)
                   ?? throw new ApplicationException($"Failed to retrieve post with ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving post {id}: {ex.Message}", ex);
        }
    }
    
    public async Task<IEnumerable<PostDto>> GetManyAsync(string? titleContains = null, string? authorUsername = null)
    {
        try
        {
            var queryParams = new List<string>();
            
            if (!string.IsNullOrWhiteSpace(titleContains))
            {
                queryParams.Add($"titleContains={Uri.EscapeDataString(titleContains)}");
            }
            
            if (!string.IsNullOrWhiteSpace(authorUsername))
            {
                queryParams.Add($"authorUsername={Uri.EscapeDataString(authorUsername)}");
            }

            var url = "Posts";
            if (queryParams.Any())
            {
                url += "?" + string.Join("&", queryParams);
            }

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<PostDto>>(jsonOptions)
                   ?? throw new ApplicationException("Failed to retrieve posts");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving posts: {ex.Message}", ex);
        }
    }

    public async Task<PostDto> UpdateAsync(int id, UpdatePostDto request)
    {
        try
        {
            var response = await client.PutAsJsonAsync($"Posts/{id}", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PostDto>(jsonOptions)
                   ?? throw new ApplicationException($"Failed to update post with ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error updating post {id}: {ex.Message}", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var response = await client.DeleteAsync($"Posts/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error deleting post {id}: {ex.Message}", ex);
        }
    }
}
