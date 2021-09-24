using Lipar.Core;
using Lipar.Entities.Domain.Organization;

namespace Lipar.Services.Organization.Contracts
{
   public interface IPositionRoleService
    {
        IPagedList<PositionRole> List(PositionRoleListVM listVM);
        void Add(PositionRole model);
        void Delete(PositionRole model);
    }
}
