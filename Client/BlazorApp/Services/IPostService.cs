using ApiContracts;
using ApiContracts.DTOs;

namespace BlazorApp.Services;

public interface IPostService
{
    Task<PostDto> CreateAsync(CreatePostDto request);
    Task<PostDto> GetByIdAsync(int id);
    Task<IEnumerable<PostDto>> GetManyAsync(string? titleContains = null, string? authorUsername = null);
    Task<PostDto> UpdateAsync(int id, UpdatePostDto request);
    Task DeleteAsync(int id);
}
