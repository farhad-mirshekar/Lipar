using Lipar.Entities.Domain.Organization;
using Lipar.Web.Areas.Admin.Models.Organization;

namespace Lipar.Web.Areas.Admin.Factories.Organization
{
    public interface IRoleModelFactory
    {
        RoleModel PrepareRoleModel(RoleModel roleModel, Role role);
        RoleListModel PrepareRoleListModel(RoleSearchModel searchModel);
    }
}
