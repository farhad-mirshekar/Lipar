using Lipar.Web.Framework.Models;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class MenuItemSearchModel : BaseSearchModel
    {
        //public MenuItemSearchModel()
        //{
        //    AddMenuItem = new MenuItemModel();
        //}
        public string Name { get; set; }
        public int MenuId { get; set; }

        //public MenuItemModel AddMenuItem { get; set; }
    }
}
