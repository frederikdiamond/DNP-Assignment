namespace BlazorApp.Services;

public class IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task UpdateUserAsync(int id, UpdateUserDto request);
    // ... more methods
}