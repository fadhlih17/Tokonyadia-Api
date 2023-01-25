using Tokonyadia_Api.Entities;

namespace Tokonyadia_Api.Services;

public interface IRoleService
{
    Task<Role> GetOrSave(ERole role);
}