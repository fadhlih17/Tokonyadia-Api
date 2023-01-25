using System.ComponentModel.DataAnnotations;

namespace Tokonyadia_Api.DTO;

public class RegisterRequest
{
    [Required, EmailAddress] public string Email { get; set; } = String.Empty;
    
    [Required] public string PhoneNumber { get; set; } = String.Empty;

    [Required, StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}