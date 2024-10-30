using ApiContracts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto)
    {
        try
        {
            var created = await _userService.CreateAsync(dto);
            return Created($"/api/users/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] string? username)
    {
        try
        {
            var users = await _userService.GetAsync(username);
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
}
    
