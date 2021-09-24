using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Organization
{
    public class RoleModelFactory : IRoleModelFactory
    {
        #region Fields
        private readonly IRoleService _roleService;
        private readonly ICommandService _commandService;
        #endregion

        #region Ctor
        public RoleModelFactory(IRoleService roleService
                              , ICommandService commandService)
        {
            _roleService = roleService;
            _commandService = commandService;
        }
        #endregion

        #region Methods
        public RoleListModel PrepareRoleListModel(RoleSearchModel searchModel)
        {
            var roles = _roleService.List(new RoleListVM { PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize });

            var model = new RoleListModel().PrepareToGrid(searchModel, roles, () =>
            {
                return roles.Select(role =>
                {
                    var roleModel = role.ToModel<RoleModel>();

                    return roleModel;
                });
            });

            return model;
        }

        public RoleModel PrepareRoleModel(RoleModel roleModel, Role role)
        {

            if (role != null)
            {
                var commands = _commandService.List(new CommandListVM { PageIndex = 0, PageSize = int.MaxValue });

                roleModel ??= role.ToModel<RoleModel>();

                roleModel.AvailableCommands = commands.Select(command => command.ToModel<CommandModel>());
                roleModel.AvailableRolePermission = role.RolePermissions.Select(rp => new RolePermissionModel { RoleId = rp.RoleId, CommandId = rp.CommandId });
            }

            return roleModel;
        }

        #endregion
    }
}
