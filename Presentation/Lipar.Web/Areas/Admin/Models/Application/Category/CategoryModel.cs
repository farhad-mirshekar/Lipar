using Lipar.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class CategoryModel : BaseEntityModel
    {
        #region Ctor
        public CategoryModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableEnableType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string NameCrumb { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public int? ParentId { get; set; }
        public int EnabledTypeId { get; set; }
        public bool IncludeInTopMenu { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableEnableType { get; set; }
        #endregion
    }
}
