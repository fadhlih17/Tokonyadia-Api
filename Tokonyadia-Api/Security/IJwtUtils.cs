using Tokonyadia_Api.Entities;

namespace Tokonyadia_Api.Security;

public interface IJwtUtils
{
    string GenerateToken(UserCredential credential);
}