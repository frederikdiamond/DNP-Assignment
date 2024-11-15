using ApiContracts;
using ApiContracts.DTOs;
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
            Username = user.Username,
            CreatedAt = user.CreatedAt,
            PostsCount = user.PostsCount,
            CommentsCount = user.CommentsCount
        };

        return Ok(userDto);
    }
}
