using Lipar.Entities.Domain.General;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class MenuController : BaseAdminController
    {
        #region Fields
        private readonly IMenuModelFactory _menuModelFactory;
        private readonly IMenuService _menuService;
        private readonly IMenuItemService _menuItemService;
        private readonly ICommandService _commandService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Ctor
        public MenuController(IMenuModelFactory menuModelFactory
                            , IMenuService menuService
                            , IMenuItemService menuItemService
                            , ICommandService commandService
                            , IActivityLogService activityLogService
                            , ILocaleStringResourceService localeStringResourceService)
        {
            _menuModelFactory = menuModelFactory;
            _menuService = menuService;
            _menuItemService = menuItemService;
            _commandService = commandService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Menu Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new MenuSearchModel());

        [HttpPost]
        public IActionResult List(MenuSearchModel searchModel)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedDataTablesJson();

            var model = _menuModelFactory.PrepareMenuListModel(searchModel);
            return Json(model);
        }

        public IActionResult Create()
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            var model = _menuModelFactory.PrepareMenuModel(new MenuModel(), null);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(MenuModel model)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var menu = model.ToEntity<Menu>();
                _menuService.Add(menu);

                // add activity log for create menu
                _activityLogService.Add("Admin.Menu.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.Menu.Create"), menu);

                return RedirectToAction("Edit", new { Id = menu.Id });
            }

            model =  _menuModelFactory.PrepareMenuModel(model, null);
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            var menu = _menuService.GetById(Id);
            if (menu == null)
                return RedirectToAction("List");

            var model = _menuModelFactory.PrepareMenuModel(null, menu);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MenuModel model)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var menu = model.ToEntity<Menu>();
                _menuService.Edit(menu);

                // add activity log for edit menu
                _activityLogService.Add("Admin.Menu.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Menu.Edit"), menu);

                return RedirectToAction("Edit", new { Id = menu.Id });
            }

            model = _menuModelFactory.PrepareMenuModel(model, null);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            var menu = _menuService.GetById(Id);
            if (menu == null)
                return RedirectToAction("List");

            _menuService.Delete(menu);

            // add activity log for delete menu
            _activityLogService.Add("Admin.Menu.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.Menu.Delete"), menu);

            return new NullJsonResult();
        }
        #endregion

        #region Menu Item Methods
        [HttpPost]
        public IActionResult MenuItems(MenuItemSearchModel searchModel)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            var model = _menuModelFactory.PrepareMenuItemListModel(searchModel);

            return Json(model);
        }

        public IActionResult MenuItemCreate(int MenuId)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            var model = _menuModelFactory.PrepareMenuItemModel(new MenuItemModel(), null);
            model.MenuId = MenuId;

            return View(model);
        }
        [HttpPost]
        public IActionResult MenuItemCreate(MenuItemModel model)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var menuItem = model.ToEntity<MenuItem>();

                _menuItemService.Add(menuItem);

                // add activity log for create menu item
                _activityLogService.Add("Admin.MenuItem.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.MenuItem.Create"), menuItem);

                return RedirectToAction("Edit", new { Id = model.MenuId });
            }

            model = _menuModelFactory.PrepareMenuItemModel(model, null);

            return View(model);
        }

        public IActionResult MenuItemEdit(int Id)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            var menuItem = _menuItemService.GetById(Id);
            if (menuItem == null)
                return RedirectToAction("List");

            var model = _menuModelFactory.PrepareMenuItemModel(null, menuItem);

            return View(model);
        }

        [HttpPost]
        public IActionResult MenuItemEdit(MenuItemModel model)
        {
            if (!_commandService.CheckPermission("ManageMenu"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var menuItem = model.ToEntity<MenuItem>();

                _menuItemService.Edit(menuItem);

                // add activity log for edit menu item
                _activityLogService.Add("Admin.MenuItem.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.MenuItem.Edit"), menuItem);

                return RedirectToAction("Edit", new { Id = model.MenuId });
            }

            model = _menuModelFactory.PrepareMenuItemModel(model, null);

            return View(model);
        }

        [HttpPost]
        public ActionResult MenuItemDelete(int Id)
        {
            var menuItem = _menuItemService.GetById(Id);
            if (menuItem == null)
                return RedirectToAction("List");

            _menuItemService.Delete(menuItem);

            // add activity log for delete menu item
            _activityLogService.Add("Admin.MenuItem.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.MenuItem.Delete"), menuItem);

            return new NullJsonResult();
        }
        #endregion
    }
}
