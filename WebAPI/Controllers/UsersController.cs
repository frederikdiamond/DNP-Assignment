using ApiContracts.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Shared.ApiContracts;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepo;

    public UsersController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto request)
    {
        /*var user = new User
        {
            Username = request.Username,
            Password = request.Password
        };*/
        
        var user = new User(request.Username, request.Password);

        var created = await _userRepo.AddAsync(user);
        var dto = new UserDto
        {
            Id = created.Id,
            Username = created.Username
        };

        return Created($"/Users/{dto.Id}", dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetSingle(int id)
    {
        var user = await _userRepo.GetSingleAsync(id);
        if (user == null)
            return NotFound();

        return Ok(new UserDto
        {
            Id = user.Id,
            Username = user.Username
        });
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetMany([FromQuery] string? username)
    {
        var query = await _userRepo.GetManyAsync();
        if (!string.IsNullOrWhiteSpace(username))
        {
            query = query.Where(u => u.Username.Contains(username));
        }

        var users = query
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username
            })
            .ToList();

        return Ok(users);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _userRepo.DeleteAsync(id);
        return NoContent();
    }
}