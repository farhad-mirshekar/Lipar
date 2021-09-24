using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class CategoryModel : BaseEntityModel
    {
        public CategoryModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }
        public string Name { get; set; }
        public string NameCrumb { get; set; }
        public int? ParentId { get; set; }

        #region Select
        public IList<SelectListItem> AvailableCategories { get; set; }
        #endregion
    }
}
