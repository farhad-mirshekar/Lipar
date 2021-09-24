using Lipar.Core;
using Lipar.Entities.Domain.Organization;

namespace Lipar.Services.Organization.Contracts
{
    public interface IRoleService
    {
        Role GetById(int Id);
        void Add(Role model);
        void Edit(Role model);
        IPagedList<Role> List(RoleListVM listVM);
    }
}
