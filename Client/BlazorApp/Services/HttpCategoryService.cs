using System.Text.Json;
using ApiContracts.DTOs;

namespace BlazorApp.Services;

public class HttpCategoryService : ICategoryService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions jsonOptions;

    public HttpCategoryService(HttpClient client)
    {
        this.client = client;
        this.jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto request)
    {
        try
        {
            var response = await client.PostAsJsonAsync("Categories", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoryDto>(jsonOptions)
                ?? throw new ApplicationException("Failed to deserialize category response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error creating category: {ex.Message}", ex);
        }
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        try
        {
            var response = await client.GetAsync($"Categories/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoryDto>(jsonOptions)
                ?? throw new ApplicationException($"Failed to retrieve category with ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving category {id}: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<CategoryDto>> GetManyAsync()
    {
        try
        {
            var response = await client.GetAsync("Categories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>(jsonOptions)
                ?? throw new ApplicationException("Failed to retrieve categories");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving categories: {ex.Message}", ex);
        }
    }

    public async Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto request)
    {
        try
        {
            var response = await client.PutAsJsonAsync($"Categories/{id}", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoryDto>(jsonOptions)
                ?? throw new ApplicationException($"Failed to update category with ID {id}");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error updating category {id}: {ex.Message}", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var response = await client.DeleteAsync($"Categories/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error deleting category {id}: {ex.Message}", ex);
        }
    }
}
