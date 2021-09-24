using Lipar.Entities.Domain.Organization;

namespace Lipar.Services.Organization.Contracts
{
    public interface IRolePermissionService
    {
        void Add(RolePermission rolePermission);
        void Delete(RolePermission rolePermission);
    }
}
