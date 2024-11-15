using ApiContracts;
using ApiContracts.DTOs;

namespace BlazorApp.Services;

public interface ICommentService
{
    Task<CommentDto> CreateAsync(CreateCommentDto request);
    Task<CommentDto> GetByIdAsync(int id);
    Task<IEnumerable<CommentDto>> GetManyAsync(string? authorUsername = null);
    Task<CommentDto> UpdateAsync(int id, UpdateCommentDto request);
    Task DeleteAsync(int id);
}
