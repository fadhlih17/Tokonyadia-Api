using System.ComponentModel.DataAnnotations;

namespace Tokonyadia_Api.DTO;

public class AuthRequest
{
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, StringLength(maximumLength:int.MaxValue, MinimumLength = 6)]
    public string Password { get; set; }
}