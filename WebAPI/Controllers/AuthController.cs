using ApiContracts.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Shared.ApiContracts.Requests;
namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest loginRequest)
    {
        var users = await _userRepository.GetMany();
        var user = users.FirstOrDefault(u => u.Username == loginRequest.Username);
        
        // Checking if user exists
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        // Verifying password
        if (user.Password != loginRequest.Password)
        {
            return Unauthorized("Invalid username or password");
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username
        };

        return Ok(userDto);
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] CreateUserDto createUserDto)
    {
        var users = await _userRepository.GetMany();
        
        if (users.Any(u => u.Username == createUserDto.Username))
        {
            return BadRequest("Username is already taken");
        }

        /*var user = new User
        {
            Username = createUserDto.Username,
            Password = createUserDto.Password
        };*/
        
        var user = new User(createUserDto.Username, createUserDto.Password);

        var createdUser = await _userRepository.AddAsync(user);

        var userDto = new UserDto
        {
            Id = createdUser.Id,
            Username = createdUser.Username
        };

        return Created($"/users/{userDto.Id}", userDto);
    }
    
    [HttpPut("users/{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetSingleAsync(id);
        
        if (user == null)
        {
            return NotFound();
        }

        // Checking if username is already taken
        if (!string.IsNullOrEmpty(updateUserDto.Username) && updateUserDto.Username != user.Username)
        {
            var users = await _userRepository.GetMany();
            if (users.Any(u => u.Username == updateUserDto.Username))
            {
                return BadRequest("Username is already taken");
            }
            user.Username = updateUserDto.Username;
        }


        if (!string.IsNullOrEmpty(updateUserDto.Password))
        {
            user.Password = updateUserDto.Password;
        }

        await _userRepository.UpdateAsync(user);

        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username
        };

        return Ok(userDto);
    }
}
