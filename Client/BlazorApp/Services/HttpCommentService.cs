using System.Text.Json;
using ApiContracts;
using ApiContracts.DTOs;

namespace BlazorApp.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions jsonOptions;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
        
        this.jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<CommentDto> CreateAsync(CreateCommentDto request)
    {
        try
        {
            var response = await client.PostAsJsonAsync("Comments", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CommentDto>(jsonOptions)
                   ?? throw new ApplicationException("Failed to deserialize comment response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error creating comment: {ex.Message}", ex);
        }
    }

    public async Task<CommentDto> GetByIdAsync(int id)
    {
        try
        {
            var response = await client.GetAsync($"Comments/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CommentDto>(jsonOptions)
                   ?? throw new ApplicationException($"Failed to retrieve comment with ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving comment {id}: {ex.Message}", ex);
        }
    }
    
    public async Task<IEnumerable<CommentDto>> GetManyAsync(string? authorUsername = null)
    {
        try
        {
            var url = "Comments";
            if (!string.IsNullOrWhiteSpace(authorUsername))
            {
                url += $"?authorUsername={Uri.EscapeDataString(authorUsername)}";
            }

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<CommentDto>>(jsonOptions)
                   ?? throw new ApplicationException("Failed to retrieve comments");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving comments: {ex.Message}", ex);
        }
    }
    
    public async Task<CommentDto> UpdateAsync(int id, UpdateCommentDto request)
    {
        try
        {
            var response = await client.PutAsJsonAsync($"Comments/{id}", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CommentDto>(jsonOptions)
                   ?? throw new ApplicationException($"Failed to update comment with ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error updating comment {id}: {ex.Message}", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var response = await client.DeleteAsync($"Comments/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error deleting comment {id}: {ex.Message}", ex);
        }
    }
}
