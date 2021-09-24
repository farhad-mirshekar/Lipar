using Lipar.Entities.Domain.General;
using Lipar.Web.Areas.Admin.Models.General;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public interface IMenuModelFactory
    {
        MenuListModel PrepareMenuListModel(MenuSearchModel searchModel);
        MenuModel PrepareMenuModel(MenuModel model, Menu menu);
        MenuItemListModel PrepareMenuItemListModel(MenuItemSearchModel searchModel);
        MenuItemModel PrepareMenuItemModel(MenuItemModel model, MenuItem menuItem);
    }
}
