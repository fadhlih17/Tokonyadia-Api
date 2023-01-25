using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Repositories;

namespace Tokonyadia_Api.Services;

public class RoleService : IRoleService
{
    private readonly IRepository<Role> _repository;
    private readonly Ipersistance _persistance;

    public RoleService(IRepository<Role> repository, Ipersistance persistance)
    {
        _repository = repository;
        _persistance = persistance;
    }
    
    public async Task<Role> GetOrSave(ERole role)
    {
        var roleFind = await _repository.Find(r => r.ERole.Equals(role));
        if (roleFind is not null) return roleFind;

        var saveRole = await _repository.Save(new Role { ERole = role });
        await _persistance.SaveChangesAsync();
        return saveRole;
    }
}