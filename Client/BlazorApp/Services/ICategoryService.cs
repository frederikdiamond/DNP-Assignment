using ApiContracts.DTOs;

namespace BlazorApp.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(CreateCategoryDto request);
    Task<CategoryDto> GetByIdAsync(int id);
    Task<IEnumerable<CategoryDto>> GetManyAsync();
    Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto request);
    Task DeleteAsync(int id);
}
