using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Organization;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class RoleController : BaseAdminController
    {
        #region Fields
        private readonly IRoleModelFactory _roleModelFactory;
        private readonly IRoleService _roleService;
        private readonly ICommandService _commandService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Ctor
        public RoleController(IRoleModelFactory roleModelFactory
                            , IRoleService roleService
                            , ICommandService commandService
                            , IRolePermissionService rolePermissionService
                            , IActivityLogService activityLogService
                            , ILocaleStringResourceService localeStringResourceService)
        {
            _roleModelFactory = roleModelFactory;
            _roleService = roleService;
            _commandService = commandService;
            _rolePermissionService = rolePermissionService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Methods
        public IActionResult Index
            => RedirectToAction("List");

        public IActionResult List()
        => View(new RoleSearchModel());

        [HttpPost]
        public IActionResult List(RoleSearchModel searchModel)
        {
            if (!_commandService.CheckPermission("ManageRole"))
                return AccessDeniedView();

            var model = _roleModelFactory.PrepareRoleListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            if (!_commandService.CheckPermission("ManageRole"))
                return AccessDeniedView();

            var model = _roleModelFactory.PrepareRoleModel(new RoleModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(RoleModel model)
        {
            if (!_commandService.CheckPermission("ManageRole"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var role = model.ToEntity<Role>();
                _roleService.Add(role);

                // add activity log for create role
                _activityLogService.Add("Admin.Role.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.Role.Create"), role);

                return RedirectToAction("Edit", new { Id = role.Id }); // redirect to edit page
            }

            model = _roleModelFactory.PrepareRoleModel(model, null);
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            if (!_commandService.CheckPermission("ManageRole"))
                return AccessDeniedView();

            var role = _roleService.GetById(Id);
            if (role == null)
                return RedirectToAction("List");

            var model = _roleModelFactory.PrepareRoleModel(null, role);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(RoleModel model, IFormCollection form)
        {
            if (!_commandService.CheckPermission("ManageRole"))
                return AccessDeniedView();

            var role = _roleService.GetById(model.Id);
            if (role == null)
                return RedirectToAction("List");

            var commands = _commandService.List(new CommandListVM { PageIndex = 0, PageSize = int.MaxValue });
            if (commands == null)
                return RedirectToAction("List");

            var rolePermissions = new List<RolePermission>();
            foreach (var command in commands)
            {
                var formKey = $"allowed_{command.Id}";

                var permissionRecordSystemNamesToRestrict = !StringValues.IsNullOrEmpty(form[formKey])
                   ? form[formKey].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                   : new List<string>();

                var allow = permissionRecordSystemNamesToRestrict.Contains(command.SystemName);

                if (allow) //کاربر انتخاب کرده
                {
                    if (role.RolePermissions.Any(rp => rp.Command.SystemName.Contains(command.SystemName)))
                        continue;
                    else
                    {
                        //insert role permission
                        var rolePermission = new RolePermission { RoleId = role.Id, CommandId = command.Id  };
                        rolePermissions.Add(rolePermission);
                        //_rolePermissionService.Add(rolePermission);
                        //rolePermission.Command = command;
                    }
                }
                else
                {
                    if (role.RolePermissions.Any(rp => rp.Command.SystemName.Contains(command.SystemName)))
                    {
                        //delete
                        _rolePermissionService.Delete(role.RolePermissions.First(rp => rp.Command.SystemName.Contains(command.SystemName)));
                    }
                }
            }

            if (rolePermissions.Any())
            {
                _rolePermissionService.Add(rolePermissions);
            }

            // add activity log for edit role
            _activityLogService.Add("Admin.Role.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Role.Edit"), role);

            model = _roleModelFactory.PrepareRoleModel(null, role);
            return View(model);
        }
        #endregion
    }
}
