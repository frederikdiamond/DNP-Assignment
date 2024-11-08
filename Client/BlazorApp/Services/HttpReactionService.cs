using System.Text.Json;
using ApiContracts;
using ApiContracts.DTOs;

namespace BlazorApp.Services;

public class HttpReactionService : IReactionService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions jsonOptions;

    public HttpReactionService(HttpClient client)
    {
        this.client = client;
        this.jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<ReactionDto> CreateAsync(CreateReactionDto request)
    {
        try
        {
            var response = await client.PostAsJsonAsync("Reactions", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReactionDto>(jsonOptions)
                ?? throw new ApplicationException("Failed to deserialize reaction response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error creating reaction: {ex.Message}", ex);
        }
    }

    public async Task<ReactionDto> GetByIdAsync(int id)
    {
        try
        {
            var response = await client.GetAsync($"Reactions/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReactionDto>(jsonOptions)
                ?? throw new ApplicationException($"Failed to retrieve reaction with ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving reaction {id}: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<ReactionDto>> GetManyAsync(int? postId = null, int? userId = null, int? commentId = null)
    {
        try
        {
            var queryParams = new List<string>();
            
            if (postId.HasValue)
                queryParams.Add($"postId={postId.Value}");
            
            if (userId.HasValue)
                queryParams.Add($"userId={userId.Value}");
            
            if (commentId.HasValue)
                queryParams.Add($"commentId={commentId.Value}");

            var url = "Reactions";
            if (queryParams.Any())
            {
                url += "?" + string.Join("&", queryParams);
            }

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ReactionDto>>(jsonOptions)
                ?? throw new ApplicationException("Failed to retrieve reactions");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving reactions: {ex.Message}", ex);
        }
    }

    public async Task<ReactionSummaryDto> GetPostSummaryAsync(int postId)
    {
        try
        {
            var response = await client.GetAsync($"Reactions/summary/post/{postId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReactionSummaryDto>(jsonOptions)
                ?? throw new ApplicationException($"Failed to retrieve post summary for post ID {postId}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving post summary: {ex.Message}", ex);
        }
    }

    public async Task<ReactionSummaryDto> GetCommentSummaryAsync(int commentId)
    {
        try
        {
            var response = await client.GetAsync($"Reactions/summary/comment/{commentId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReactionSummaryDto>(jsonOptions)
                ?? throw new ApplicationException($"Failed to retrieve comment summary for comment ID {commentId}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving comment summary: {ex.Message}", ex);
        }
    }

    public async Task<ReactionDto> UpdateVoteAsync(int id, bool isUpvote)
    {
        try
        {
            var response = await client.PutAsync($"Reactions/{id}/vote?isUpvote={isUpvote}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReactionDto>(jsonOptions)
                ?? throw new ApplicationException($"Failed to update vote for reaction ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error updating vote: {ex.Message}", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var response = await client.DeleteAsync($"Reactions/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error deleting reaction {id}: {ex.Message}", ex);
        }
    }
}
