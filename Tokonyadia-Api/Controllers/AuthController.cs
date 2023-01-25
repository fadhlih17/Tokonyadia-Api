using Microsoft.AspNetCore.Mvc;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;

namespace Tokonyadia_Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private static List<UserCredential> _userCredentials = new();

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        return null;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        return null;
    }
}