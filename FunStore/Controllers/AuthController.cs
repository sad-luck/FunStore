using FunStore.Models.Request;
using FunStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(
        IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequestModel)
    {
        string token = await _userService.Login(loginRequestModel.Username, loginRequestModel.Password);

        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerRequestModel)
    {
        await _userService.Register(registerRequestModel.Username,
            registerRequestModel.Password,
            registerRequestModel.FirstName,
            registerRequestModel.LastName);

        return Ok(new { Message = $"Hi, {registerRequestModel.Username}! Your registration was successful." });
    }
}