using ApiContracts;

namespace BlazorApp.Services;
using System.Text.Json;
using System.Net.Http.Json;


public class HttpUserService : IUserService
{
    private readonly HttpClient httpClient;

    public HttpUserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var response = await httpClient.PostAsJsonAsync("api/users", dto);
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
        var users = await httpClient.GetFromJsonAsync<IEnumerable<UserDto>>(url);
        return users ?? Enumerable.Empty<UserDto>();
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await httpClient.GetFromJsonAsync<UserDto>($"api/users/{id}");
        if (user == null)
        {
            throw new Exception($"User with ID {id} not found.");
        }
        return user;
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto)
    {
        var response = await httpClient.PutAsJsonAsync($"api/users/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"api/users/{id}");
        response.EnsureSuccessStatusCode();
    }
    
    public async Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}