using ApiContracts.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoriesController(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto request)
    {
        /*var category = new Category
        {
            Name = request.Name,
            Description = request.Description
        };*/
        
        var category = new Category(request.Name, request.Description);

        var created = await _categoryRepo.AddAsync(category);
        var dto = new CategoryDto
        {
            Id = created.Id,
            Name = created.Name,
            Description = created.Description
        };

        return Created($"/Categories/{dto.Id}", dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetSingle(int id)
    {
        var category = await _categoryRepo.GetSingleAsync(id);
        if (category == null)
            return NotFound();

        return Ok(new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        });
    }

    [HttpGet]
    public ActionResult<List<CategoryDto>> GetMany()
    {
        var categories = _categoryRepo.GetMany()
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .ToList();

        return Ok(categories);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] UpdateCategoryDto request)
    {
        var category = await _categoryRepo.GetSingleAsync(id);
        if (category == null)
            return NotFound();

        category.Name = request.Name;
        category.Description = request.Description;

        await _categoryRepo.UpdateAsync(category);

        return Ok(new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _categoryRepo.DeleteAsync(id);
        return NoContent();
    }
}
