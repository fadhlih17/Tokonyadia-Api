using System.ComponentModel.DataAnnotations;

namespace Tokonyadia_Api.DTO;

public class LoginRequest
{
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;

    [Required, StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}