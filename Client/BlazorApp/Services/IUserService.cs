using ApiContracts;

namespace BlazorApp.Services;

public interface IUserService
{
    Task<UserDto> CreateAsync(CreateUserDto request);
    Task<UserDto> GetByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetManyAsync(string? username = null);
    Task<UserDto> UpdateAsync(int id, UpdateUserDto request);
    Task DeleteAsync(int id);
}
