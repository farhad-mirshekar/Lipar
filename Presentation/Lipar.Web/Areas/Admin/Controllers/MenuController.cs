using Lipar.Entities.Domain.General;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC;
using Microsoft.AspNetCore.Mvc;
using System;
using Lipar.Web.Framework.MVC.Filters;
using Lipar.Web.Areas.Admin.Helpers;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class MenuController : BaseAdminController
    {
        #region Fields
        private readonly IMenuModelFactory _menuModelFactory;
        private readonly IMenuService _menuService;
        private readonly IMenuItemService _menuItemService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Ctor
        public MenuController(IMenuModelFactory menuModelFactory
                            , IMenuService menuService
                            , IMenuItemService menuItemService
                            , IActivityLogService activityLogService
                            , ILocaleStringResourceService localeStringResourceService)
        {
            _menuModelFactory = menuModelFactory;
            _menuService = menuService;
            _menuItemService = menuItemService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Menu Methods

        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult List()
            => View(new MenuSearchModel());

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult List(MenuSearchModel searchModel)
        {
            var model = _menuModelFactory.PrepareMenuListModel(searchModel);
            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult Create()
        {
            var model = _menuModelFactory.PrepareMenuModel(new MenuModel(), null);
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult Create(MenuModel model)
        {
            if (ModelState.IsValid)
            {
                var menu = model.ToEntity<Menu, Guid>();
                _menuService.Add(menu);

                // add activity log for create menu
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.Menu.Create"), menu);

                return RedirectToAction("Edit", new { Id = menu.Id });
            }

            model =  _menuModelFactory.PrepareMenuModel(model, null);
            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult Edit(Guid Id)
        {
            var menu = _menuService.GetById(Id);
            if (menu == null)
                return RedirectToAction("List");

            var model = _menuModelFactory.PrepareMenuModel(null, menu);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult Edit(MenuModel model)
        {
            if (ModelState.IsValid)
            {
                var menu = model.ToEntity<Menu, Guid>();
                _menuService.Edit(menu);

                // add activity log for edit menu
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Menu.Edit"), menu);

                return RedirectToAction("Edit", new { Id = menu.Id });
            }

            model = _menuModelFactory.PrepareMenuModel(model, null);
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult Delete(Guid Id)
        {
            var menu = _menuService.GetById(Id);
            if (menu == null)
                return RedirectToAction("List");

            _menuService.Delete(menu);

            // add activity log for delete menu
            _activityLogService.Add("Admin.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.Menu.Delete"), menu);

            return new NullJsonResult();
        }
        #endregion

        #region Menu Item Methods

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult MenuItems(MenuItemSearchModel searchModel)
        {
            var model = _menuModelFactory.PrepareMenuItemListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult MenuItemCreate(Guid MenuId)
        {
            var model = _menuModelFactory.PrepareMenuItemModel(new MenuItemModel(), null);
            model.MenuId = MenuId;

            return View(model);
        }
        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult MenuItemCreate(MenuItemModel model)
        {
            if (ModelState.IsValid)
            {
                var menuItem = model.ToEntity<MenuItem, Guid>();

                _menuItemService.Add(menuItem);

                // add activity log for create menu item
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.MenuItem.Create"), menuItem);

                return RedirectToAction("Edit", new { Id = model.MenuId });
            }

            model = _menuModelFactory.PrepareMenuItemModel(model, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult MenuItemEdit(Guid Id)
        {
            var menuItem = _menuItemService.GetById(Id);
            if (menuItem == null)
                return RedirectToAction("List");

            var model = _menuModelFactory.PrepareMenuItemModel(null, menuItem);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public IActionResult MenuItemEdit(MenuItemModel model)
        {
            if (ModelState.IsValid)
            {
                var menuItem = model.ToEntity<MenuItem, Guid>();

                _menuItemService.Edit(menuItem);

                // add activity log for edit menu item
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.MenuItem.Edit"), menuItem);

                return RedirectToAction("Edit", new { Id = model.MenuId });
            }

            model = _menuModelFactory.PrepareMenuItemModel(model, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMenu)]
        public ActionResult MenuItemDelete(Guid Id)
        {
            var menuItem = _menuItemService.GetById(Id);
            if (menuItem == null)
                return RedirectToAction("List");

            _menuItemService.Delete(menuItem);

            // add activity log for delete menu item
            _activityLogService.Add("Admin.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.MenuItem.Delete"), menuItem);

            return new NullJsonResult();
        }
        #endregion
    }
}
