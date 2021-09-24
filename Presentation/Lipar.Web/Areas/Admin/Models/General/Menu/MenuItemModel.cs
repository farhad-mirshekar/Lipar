using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class MenuItemModel : BaseEntityModel
    {
        #region Ctor
        public MenuItemModel()
        {
            AvailableViewStatusType = new List<SelectListItem>();
            AvailableForeignLinkType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public int ViewStatusId { get; set; }
        public string Url { get; set; }
        public string IconText { get; set; }
        public int Priority { get; set; }
        public string Parameters { get; set; }
        public string TitleCrumb { get; set; }
        public int? ParentId { get; set; }
        public int MenuId { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableViewStatusType { get; set; }
        public IList<SelectListItem> AvailableForeignLinkType { get; set; }
        #endregion
    }
}
