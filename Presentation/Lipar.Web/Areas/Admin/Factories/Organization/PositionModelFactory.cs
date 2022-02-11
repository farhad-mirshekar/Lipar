using Lipar.Core.Caching;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Cache;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Organization
{
    public class PositionModelFactory : IPositionModelFactory
    {
        #region Fields
        private readonly IPositionService _positionService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IRoleService _roleService;
        private readonly IDepartmentService _departmentService;
        private readonly IStaticCacheManager _staticCacheManager;
        #endregion

        #region Ctor
        public PositionModelFactory(IPositionService positionService
                                  , IBaseAdminModelFactory baseAdminModelFactory
                                  , IRoleService roleService
                                  , IDepartmentService departmentService
                                  , IStaticCacheManager staticCacheManager)
        {
            _positionService = positionService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _roleService = roleService;
            _departmentService = departmentService;
            _staticCacheManager = staticCacheManager;
        }
        #endregion

        #region Methods
        public PositionListModel PreparePositionListModel(PositionSearchModel searchModel)
        {
            var positions = _positionService.List(new PositionListVM
            {
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
                DepartmentId = searchModel.DepartmentId,
                EnabledTypeId = searchModel.EnabledTypeId,
                UserListVM = new UserListVM
                {
                    FirstName = searchModel.FirstName,
                    LastName = searchModel.LastName,
                    NationalCode = searchModel.NationalCode
                }
            });

            if (positions == null)
                return null;

            var model = new PositionListModel().PrepareToGrid(searchModel, positions, () =>
            {
                return positions.Select(position =>
                 {
                     var positionModel = position.ToModel<PositionModel, Guid>();

                     return positionModel;
                 });
            });

            return model;
        }

        public PositionModel PreparePositionModel(PositionModel model, Position position)
        {
            if (position != null)
            {
                model = position.ToModel<PositionModel, Guid>();

                //get selected roles by position
                model.AvailablePositionRole = GetPositionRoles(position);
            }

            //get all role
            model.AvailableRoles = GetsRoles(new RoleListVM { PageIndex = 0, PageSize = int.MaxValue });

            _baseAdminModelFactory.PrepareEnabledType(model.AvailableEnableType);
            _baseAdminModelFactory.PreparePositionType(model.AvailablePositionType);
            model.AvailableDepartments = _baseAdminModelFactory.PrepareDepartment();

            return model;
        }

        public PositionSearchModel PreparePositionSearchModel()
        {
            var positionSearchModel = new PositionSearchModel();
            positionSearchModel.EnabledTypeId = (int)EnabledTypeEnum.Active;

            //gets all department
            positionSearchModel.AvailableDepartments = _baseAdminModelFactory.PrepareDepartment();

            //gets enabled type
            _baseAdminModelFactory.PrepareEnabledType(positionSearchModel.AvailableEnabledType);

            return positionSearchModel;
        }
        #endregion

        #region Utilities
        private IEnumerable<RoleModel> GetsRoles(RoleListVM listVM)
        {
            var roles = _roleService.List(new RoleListVM { PositionId = listVM.PositionId, UserId = listVM.UserId, PageIndex = listVM.PageIndex, PageSize = listVM.PageSize });

            var model = roles.Select(role =>
            {
                var rolesModel = role.ToModel<RoleModel, Guid>();

                return rolesModel;
            });

            return model;
        }
        private IEnumerable<PositionRoleModel> GetPositionRoles(Position position)
        {
            var positionRoleModel = new List<PositionRoleModel>();

            position = _positionService.GetById(position.Id);
            if (position != null)
            {
                if (position.PositionRoles != null)
                {
                    foreach (var positionRole in position.PositionRoles)
                    {
                        positionRoleModel.Add(positionRole.ToModel<PositionRoleModel, Guid>());
                    }
                }
            }
            return positionRoleModel;
        }
        #endregion
    }
}
