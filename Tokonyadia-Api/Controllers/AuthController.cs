using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Services;

namespace Tokonyadia_Api.Controllers;

// [ApiController]
[Route("api/auth")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous] // Semua orang bisa akses tanpa membawa token
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = await _authService.Register(request);
        var response = new CommonResponse<RegisterResponse>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Succesfully create new user",
            Data = user
        };
        
        return Created("api/auth/register", response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var login = await _authService.Login(request);
        var response = new CommonResponse<LoginResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Succesfully Login",
            Data = login
        };

        return Ok(response);
    }
    
    
    // Admin
    [HttpPost("register-admin")]
    [AllowAnonymous] // Semua orang bisa akses tanpa membawa token
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
    {
        var user = await _authService.RegisterAdmin(request);
        var response = new CommonResponse<RegisterResponse>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Successfully create new admin",
            Data = user
        };
        
        return Created("api/auth/register-admin", response);
    }

}