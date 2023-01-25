using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;

namespace Tokonyadia_Api.Services;

public interface IAuthService
{
    Task<UserCredential> LoadByEmail(string email);
    Task<RegisterResponse> Register(RegisterRequest request);
    Task<LoginResponse> Login(LoginRequest request);
    Task<RegisterResponse> RegisterAdmin(RegisterRequest request);
}