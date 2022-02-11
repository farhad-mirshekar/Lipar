using Lipar.Core;
using Lipar.Entities.Domain.Organization;
using System;

namespace Lipar.Services.Organization.Contracts
{
    public interface IRoleService
    {
        Role GetById(Guid Id);
        void Add(Role model);
        void Edit(Role model);
        IPagedList<Role> List(RoleListVM listVM);
    }
}
