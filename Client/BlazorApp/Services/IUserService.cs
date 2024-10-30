using ApiContracts;

namespace BlazorApp.Services;

public interface IUserService
{
   
    Task<UserDto> CreateAsync(CreateUserDto dto);
    Task<IEnumerable<UserDto>> GetAsync(string? username);
    Task<UserDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, UpdateUserDto dto);
    Task DeleteAsync(int id);
}