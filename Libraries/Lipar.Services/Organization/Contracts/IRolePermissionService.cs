using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Services.Organization.Contracts
{
    public interface IRolePermissionService
    {
        void Add(RolePermission rolePermission);
        void Delete(RolePermission rolePermission);
        
        /// <summary>
        /// add method
        /// </summary>
        /// <param name="rolePermissions"></param>
        void Add(IEnumerable<RolePermission> rolePermissions);
    }
}
