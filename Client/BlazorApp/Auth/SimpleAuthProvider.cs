using System.Security.Claims;
using System.Text.Json;
using ApiContracts.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using LoginRequest = Shared.ApiContracts.Requests.LoginRequest;

namespace BlazorApp.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient httpClient;
    private ClaimsPrincipal currentClaimsPrincipal;

    public SimpleAuthProvider(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        currentClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authState = new AuthenticationState(currentClaimsPrincipal);
        return Task.FromResult(authState);
    }
    
    public async Task Login(string userName, string password)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(
            "auth/login",
            new LoginRequest
            {
                Username = userName, 
                Password = password
            });

        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        UserDto userDto = JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;


        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.Id.ToString()),
            new Claim("CreatedAt", userDto.CreatedAt.ToString("o")),
            new Claim("PostsCount", userDto.PostsCount.ToString()),
            new Claim("CommentsCount", userDto.CommentsCount.ToString())
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        currentClaimsPrincipal = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentClaimsPrincipal))
        );
    }

    public void Logout()
    {
        currentClaimsPrincipal = new();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));
    }
}
