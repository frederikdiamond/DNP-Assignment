using ApiContracts;

namespace BlazorApp.Services;
using System.Text.Json;
using System.Net.Http.Json;


public class HttpUserService : IUserService
{
    private readonly HttpClient _httpClient;

    public HttpUserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>()!;
    }

    public async Task<IEnumerable<UserDto>> GetAsync(string? username)
    {
        var url = "api/users";
        if (!string.IsNullOrEmpty(username))
        {
            url += $"?username={username}";
        }
        var users = await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>(url);
        return users ?? Enumerable.Empty<UserDto>();
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _httpClient.GetFromJsonAsync<UserDto>($"api/users/{id}");
        if (user == null)
        {
            throw new Exception($"User with ID {id} not found.");
        }
        return user;
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/users/{id}");
        response.EnsureSuccessStatusCode();
    }
}