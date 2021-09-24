using Lipar.Core;
using Lipar.Entities.Domain.Organization;

namespace Lipar.Services.Organization.Contracts
{
    public interface IDepartmentService
    {
        IPagedList<Department> List(DepartmentListVM listVM);
    }
}
