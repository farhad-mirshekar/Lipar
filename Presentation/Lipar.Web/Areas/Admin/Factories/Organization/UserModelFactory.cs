using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Areas.Admin.Models.Organization.User;
using Lipar.Web.Framework.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Organization
{
    public class UserModelFactory : IUserModelFactory
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPositionService _positionService;
        private readonly IPositionRoleService _positionRoleService;
        #endregion

        #region Ctor
        public UserModelFactory(IUserService userService
                              , IRoleService roleService
                              , IPositionService positionService
                              , IPositionRoleService positionRoleService)
        {
            _userService = userService;
            _roleService = roleService;
            _positionService = positionService;
            _positionRoleService = positionRoleService;
        }
        #endregion

        #region Methods
        public UserListModel PrepareUserListModel(UserSearchModel searchModel)
        {
            var users = _userService.List(new UserListVM { PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize });

            var model = new UserListModel().PrepareToGrid(searchModel, users, () =>
            {
                return users.Select(user =>
                {
                    var userModel = user.ToModel<UserModel>();
                    return userModel;
                });
            });

            return model;
        }

        public UserModel PrepareUserModel(UserModel model, User user)
        {
            if (user != null)
            {
                model = user.ToModel<UserModel>();
            }

            //get all role
            model.AvailableRoles = GetsRoles(new RoleListVM { PageIndex = 0, PageSize = int.MaxValue });

            //get selected roles by position user
            model.AvailablePositionRole = GetPositionRoles(model.Id);
            return model;
        }
        #endregion

        #region Utilities
        private IEnumerable<RolesModel> GetsRoles(RoleListVM listVM)
        {
            var roles = _roleService.List(new RoleListVM { PositionId = listVM.PositionId, UserId = listVM.UserId, PageIndex = listVM.PageIndex, PageSize = listVM.PageSize });

            var model = roles.Select(role =>
            {
                var rolesModel = role.ToModel<RolesModel>();

                return rolesModel;
            });

            return model;
        }
        private IEnumerable<PositionRoleModel> GetPositionRoles(int UserId)
        {
            var positionRoleModel = new List<PositionRoleModel>();

            //get all position by user id
            var positions = _positionService.List(new PositionListVM { UserId = UserId });
            if (positions != null)
            {
                foreach (var position in positions)
                {
                    if (position.PositionRoles != null && position.PositionRoles.Count > 0)
                    {
                        foreach (var positionRole in position.PositionRoles)
                        {
                            positionRoleModel.Add(positionRole.ToModel<PositionRoleModel>());
                        }
                    }
                }
            }
            return positionRoleModel;
        }
        #endregion
    }
}
