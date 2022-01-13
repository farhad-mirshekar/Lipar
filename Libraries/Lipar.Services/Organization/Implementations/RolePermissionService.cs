using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using System;
using System.Collections.Generic;

namespace Lipar.Services.Organization.Implementations
{
    public class RolePermissionService : IRolePermissionService
    {
        #region Fields
        private readonly IRepository<RolePermission> _repository;
        #endregion

        #region Ctor
        public RolePermissionService(IRepository<RolePermission> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(RolePermission rolePermission)
        => _repository.Insert(rolePermission);

        public void Add(IEnumerable<RolePermission> rolePermissions)
        {
            if (rolePermissions == null)
            {
                throw new ArgumentNullException(nameof(rolePermissions));
            }

            _repository.Insert(rolePermissions);
        }

        public void Delete(RolePermission rolePermission)
        => _repository.Delete(rolePermission);

        #endregion
    }
}
