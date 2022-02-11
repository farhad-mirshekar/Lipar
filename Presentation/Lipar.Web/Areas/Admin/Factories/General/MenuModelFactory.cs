using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class MenuModelFactory : IMenuModelFactory
    {
        #region Fields
        private readonly IMenuService _menuService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IMenuItemService _menuItemService;
        #endregion

        #region Ctor
        public MenuModelFactory(IMenuService menuService
                              , IBaseAdminModelFactory baseAdminModelFactory
                              , IMenuItemService menuItemService)
        {
            _menuService = menuService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _menuItemService = menuItemService;
        }
        #endregion

        #region Methods
        public MenuListModel PrepareMenuListModel(MenuSearchModel searchModel)
        {
            var menus = _menuService.List(new MenuListVM { PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize, Name = searchModel.Name });

            var model = new MenuListModel().PrepareToGrid(searchModel, menus, () =>
            {
                return menus.Select(menu =>
                 {
                     var menuModel = menu.ToModel<MenuModel, Guid>();

                     return menuModel;
                 });
            });

            return model;
        }

        public MenuModel PrepareMenuModel(MenuModel model, Menu menu)
        {
            if (menu != null)
            {
                model = menu.ToModel<MenuModel, Guid>();
                PrepareMenuItemSearchModel(model.MenuItemSearchModel, menu);
            }


            _baseAdminModelFactory.PrepareAllLanguage(model.AvailableLanguageType);

            return model;
        }

        public MenuItemListModel PrepareMenuItemListModel(MenuItemSearchModel searchModel)
        {
            var menuItems = _menuItemService.List(new MenuItemListVM { PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize, MenuId = searchModel.MenuId });

            var model = new MenuItemListModel().PrepareToGrid(searchModel, menuItems, () =>
            {
                return menuItems.Select(menu =>
                {
                    var menuItemModel = menu.ToModel<MenuItemModel, Guid>();

                    return menuItemModel;
                });
            });

            return model;
        }

        public MenuItemModel PrepareMenuItemModel(MenuItemModel model, MenuItem menuItem)
        {
            if (menuItem != null)
                model = menuItem.ToModel<MenuItemModel, Guid>();

            _baseAdminModelFactory.PrepareForeignLinkType(model.AvailableForeignLinkType);
            _baseAdminModelFactory.PrepareViewStatusType(model.AvailableViewStatusType);

            return model;
        }
        #endregion

        #region Utilities
        protected MenuItemSearchModel PrepareMenuItemSearchModel(MenuItemSearchModel searchModel, Menu menu)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (menu == null)
                throw new ArgumentNullException(nameof(menu));

            searchModel.MenuId = menu.Id;

            //prepare page parameters
            searchModel.SetGridPageSize();

            //_baseAdminModelFactory.PrepareViewStatusType(searchModel.AddMenuItem.AvailableViewStatusType);
            //_baseAdminModelFactory.PrepareForeignLinkType(searchModel.AddMenuItem.AvailableForeignLinkType);

            return searchModel;
        }
        #endregion
    }
}
