using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CategoryFileRepository : ICategoryRepository
{
    private readonly string filePath = "categories.json";

    public CategoryFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Category> AddAsync(Category category)
    {
        List<Category> categories = await LoadCategoriesAsync();
        int maxId = categories.Count > 0 ? categories.Max(c => c.Id) : 1;
        category.Id = maxId + 1;
        categories.Add(category);
        await SaveCategoriesAsync(categories);
        return category;
    }

    public async Task UpdateAsync(Category category)
    {
        List<Category> categories = await LoadCategoriesAsync();
        int index = categories.FindIndex(c => c.Id == category.Id);
        if (index != -1)
        {
            categories[index] = category;
            await SaveCategoriesAsync(categories);
        }
        else
        {
            throw new KeyNotFoundException($"Category with ID {category.Id} not found.");
        }
    }

    public async Task DeleteAsync(int id)
    {
        List<Category> categories = await LoadCategoriesAsync();
        Category categoryToRemove = categories.FirstOrDefault(c => c.Id == id);
        if (categoryToRemove != null)
        {
            categories.Remove(categoryToRemove);
            await SaveCategoriesAsync(categories);
        }
        else
        {
            throw new KeyNotFoundException($"Category with ID {id} not found.");
        }
    }

    public async Task<Category> GetSingleAsync(int id)
    {
        List<Category> categories = await LoadCategoriesAsync();
        Category category = categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {id} not found.");
        }
        return category;
    }

    public IQueryable<Category> GetMany()
    {
        string categoriesAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Category> categories = JsonSerializer.Deserialize<List<Category>>(categoriesAsJson)!;
        return categories.AsQueryable();
    }

    private async Task<List<Category>> LoadCategoriesAsync()
    {
        string categoriesAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Category>>(categoriesAsJson) ?? new List<Category>();
    }

    private async Task SaveCategoriesAsync(List<Category> categories)
    {
        string categoriesAsJson = JsonSerializer.Serialize(categories);
        await File.WriteAllTextAsync(filePath, categoriesAsJson);
    }
}