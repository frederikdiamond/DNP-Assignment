using ApiContracts;
using ApiContracts.DTOs;

namespace BlazorApp.Services;
using System.Text.Json;
using System.Net.Http.Json;


public class HttpUserService : IUserService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions jsonOptions;

    public HttpUserService(HttpClient client)
    {
        this.client = client;
        
        this.jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<UserDto> CreateAsync(CreateUserDto request)
    {
        try
        {
            var response = await client.PostAsJsonAsync("Users", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDto>(jsonOptions) 
                   ?? throw new ApplicationException("Failed to deserialize user response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error creating user: {ex.Message}", ex);
        }
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        try
        {
            var response = await client.GetAsync($"Users/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDto>(jsonOptions) 
                   ?? throw new ApplicationException($"Failed to retrieve user with ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving user {id}: {ex.Message}", ex);
        }
    }
    
    public async Task<IEnumerable<UserDto>> GetManyAsync(string? username = null)
    {
        try
        {
            var url = "Users";
            if (!string.IsNullOrWhiteSpace(username))
            {
                url += $"?username={Uri.EscapeDataString(username)}";
            }

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>(jsonOptions) 
                   ?? throw new ApplicationException("Failed to retrieve users");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving users: {ex.Message}", ex);
        }
    }

    public async Task<UserDto> UpdateAsync(int id, UpdateUserDto request)
    {
        try
        {
            var response = await client.PutAsJsonAsync($"Users/{id}", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDto>(jsonOptions) 
                   ?? throw new ApplicationException($"Failed to update user with ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error updating user {id}: {ex.Message}", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var response = await client.DeleteAsync($"Users/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error deleting user {id}: {ex.Message}", ex);
        }
    }
}
