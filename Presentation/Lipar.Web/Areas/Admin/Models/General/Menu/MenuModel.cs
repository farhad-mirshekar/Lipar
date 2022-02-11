using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class MenuModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public MenuModel()
        {
            AvailableLanguageType = new List<SelectListItem>();
            MenuItemSearchModel = new MenuItemSearchModel();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public MenuItemSearchModel MenuItemSearchModel { get; set; }
        #endregion

        #region Select 
        public IList<SelectListItem> AvailableLanguageType { get; set; }
        #endregion
    }
}
