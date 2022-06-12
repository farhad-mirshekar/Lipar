using Lipar.Core;
using Lipar.Core.Infrastructure;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Organization;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using Lipar.Web.Framework.MVC.Filters;
using Lipar.Web.Areas.Admin.Helpers;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class PositionController : BaseAdminController
    {
        #region Fields
        private readonly IPositionService _positionService;
        private readonly IPositionModelFactory _positionModelFactory;
        private readonly IUserService _userService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IUserPasswordService _passwordService;
        private readonly IRoleService _roleService;
        private readonly IPositionRoleService _positionRoleService;
        private readonly ICommandService _commandService;
        #endregion

        #region Ctor
        public PositionController(IPositionService positionService
                                , IPositionModelFactory positionModelFactory
                                , IUserService userService
                                , IActivityLogService activityLogService
                                , ILocaleStringResourceService localeStringResourceService
                                , IUserPasswordService passwordService
                                , IRoleService roleService
                                , IPositionRoleService positionRoleService
                                , ICommandService commandService)
        {
            _positionService = positionService;
            _positionModelFactory = positionModelFactory;
            _userService = userService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _passwordService = passwordService;
            _roleService = roleService;
            _positionRoleService = positionRoleService;
            _commandService = commandService;
        }
        #endregion

        #region Methods

        [CheckingPermissions(permissions: CommandNames.ManagePosition)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManagePosition)]
        public IActionResult List()
        {
            var model = _positionModelFactory.PreparePositionSearchModel();
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManagePosition)]
        public IActionResult List(PositionSearchModel searchModel)
        {
            var model = _positionModelFactory.PreparePositionListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManagePosition)]
        public IActionResult Edit(Guid Id)
        {
            var position = _positionService.GetById(Id);
            if (position == null)
                return RedirectToAction("List");

            var model = _positionModelFactory.PreparePositionModel(null, position);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManagePosition)]
        public IActionResult Edit(PositionModel model, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var position = _positionService.GetById(model.Id);
                if (position == null)
                    return RedirectToAction("List");

                var user = _userService.GetById(position.UserId);
                if (user == null)
                    return RedirectToAction("List");

                #region Edit User
                if (user.FirstName != model.FirstName ||
                    user.LastName != model.LastName ||
                    user.NationalCode != model.NationalCode ||
                    user.Email != model.Email ||
                    user.CellPhone != model.CellPhone)
                {
                    if (user.FirstName != model.FirstName)
                        user.FirstName = model.FirstName.Trim();

                    if (user.LastName != model.LastName)
                        user.LastName = model.LastName.Trim();

                    if (user.NationalCode != model.NationalCode)
                        user.NationalCode = model.NationalCode.Trim();

                    if (user.Email != model.Email)
                        user.Email = model.Email.Trim();

                    if (user.CellPhone != model.CellPhone)
                        user.CellPhone = model.CellPhone.Trim();

                    _userService.Edit(user);

                    //add activity log for edit user
                    _activityLogService.Add("Admin.User.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.User.Edit"), user);
                }

                #endregion

                ModifyRoleSelected(position, form);

                //add activity log for edit position
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Position.Edit"), user);

                return RedirectToAction("Edit", new { Id = position.Id });
            }

            model = _positionModelFactory.PreparePositionModel(model, null);
            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManagePosition)]
        public IActionResult Create()
        {
            var model = _positionModelFactory.PreparePositionModel(new PositionModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManagePosition)]
        public IActionResult Create(PositionModel model, IFormCollection form)
        {
            if (ModelState.IsValid)
            {

                #region Create User
                var user = new User
                {
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    NationalCode = model.NationalCode.Trim(),
                    CellPhone = model.CellPhone.Trim(),
                    CellPhoneVerified = true,
                    Email = model.Email.Trim(),
                    EmailVerified = true,
                    Username = model.NationalCode.Trim(),
                    UserTypeId = (int)UserTypeEnum.Admin,
                    EnabledTypeId = (int)EnabledTypeEnum.Active
                };

                _userService.Add(user);

                //add activity log for create user
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.User.Create"), user);

                var password = new UserPassword
                {
                    Password = model.NationalCode.Trim(),
                    PasswordFormatTypeId = (int)PasswordFormatTypeEnum.Encrypted,
                    UserId = user.Id
                };

                _passwordService.Add(password);

                //add activity log for create password
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.UserPassword.Create"), password);
                #endregion

                #region Create Position
                var position = new Position
                {
                    DepartmentId = model.DepartmentId,
                    EnabledTypeId = (int)EnabledTypeEnum.Active,
                    Default = true,
                    PositionTypeId = (int)PositionTypeEnum.Managment,
                    UserId = user.Id
                };

                _positionService.Add(position);
                #endregion

                #region Position Role
                //get all role
                var roles = _roleService.List(new RoleListVM { });
                foreach (var role in roles)
                {
                    //create form key for check selected checkbox
                    var formKey = $"allowed_{role.Id}";

                    var roleSelected = !StringValues.IsNullOrEmpty(form[formKey])
                      ? form[formKey].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                      : new List<string>();

                    var allowed = roleSelected.Contains(role.Id.ToString());

                    if (allowed) //کاربر انتخاب کرده
                    {
                        //insert position role
                        _positionRoleService.Add(new PositionRole { RoleId = role.Id, PositionId = position.Id });
                    }
                }
                #endregion

                //add activity log for edit position
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.Position.Create"), user);

                return RedirectToAction("Edit", new { Id = position.Id });
            }

            model = _positionModelFactory.PreparePositionModel(model, null);
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManagePosition)]
        public IActionResult Delete(Guid Id)
        {
            var position = _positionService.GetById(Id);
            if (position == null)
                return RedirectToAction("List");

            //step 1 : check position role and delete 
            if (position.PositionRoles != null && position.PositionRoles.Count() > 0)
            {
                foreach (var positionRole in position.PositionRoles)
                    _positionRoleService.Delete(positionRole);
            }

            //step 2:delete position
            _positionService.Delete(position);

            return RedirectToAction("List");
        }

        public IActionResult ChangePosition(Guid Id)
        {
            
            if (Id == Guid.Empty)
            {
                return RedirectToRoute("areaRoute", new { area = AreaNames.Admin, controller = "Home", action = "Index" });
            }

            var _workContext = EngineContext.Current.Resolve<IWorkContext>();
            var positions = new List<Position>();

            var currentPosition = _positionService.GetById(_workContext.CurrentPosition.Id);
            var position = _positionService.GetById(Id);

            if (position == null)
            {
                return RedirectToRoute("areaRoute", new { area = AreaNames.Admin, controller = "Home", action = "Index" });
            }

            if (position.EnabledTypeId == (int)EnabledTypeEnum.InActive)
            {
                return RedirectToRoute("areaRoute", new { area = AreaNames.Admin, controller = "Home", action = "Index" });
            }

            currentPosition.Default = false;
            position.Default = true;

            positions.Add(currentPosition);
            positions.Add(position);

            _positionService.Edit(positions);

            _workContext.CurrentPosition = position;

            var roleId = position.PositionRoles.Select(p => p.RoleId).First();
            var commands = _commandService.List(new CommandListVM { RoleId = roleId });

            if (commands.Count() == 0)
                return null;

            _workContext.Commands = commands;

            return RedirectToRoute("areaRoute", new { area = AreaNames.Admin, controller = "Home", action = "Index" });
        }

        #endregion

        #region Utilities
        private void ModifyRoleSelected(Position position, IFormCollection form)
        {
            //get all role
            var roles = _roleService.List(new RoleListVM { });
            foreach (var role in roles)
            {
                //create form key for check selected checkbox
                var formKey = $"allowed_{role.Id}";

                var roleSelected = !StringValues.IsNullOrEmpty(form[formKey])
                  ? form[formKey].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                  : new List<string>();

                var allowed = roleSelected.Contains(role.Id.ToString());

                if (allowed) //کاربر انتخاب کرده
                {
                    if (position.PositionRoles.Any(pr => pr.RoleId.Equals(role.Id)))
                        continue;
                    else
                        //insert position role
                        _positionRoleService.Add(new PositionRole { RoleId = role.Id, PositionId = position.Id });
                }
                else
                {
                    if (position.PositionRoles.Any(pr => pr.RoleId.Equals(role.Id)))
                    {
                        //delete
                        _positionRoleService.Delete(position.PositionRoles.First(pr => pr.RoleId.Equals(role.Id)));
                    }
                }
            }
        }
        #endregion
    }
}
