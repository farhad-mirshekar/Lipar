using Lipar.Entities;
using Lipar.Entities.Domain.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class StaticPageModel : BaseEntityModel
    {
        #region Ctor
        public StaticPageModel()
        {
            AvailableViewStatusType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public int Priority { get; set; }
        public int ViewStatusId { get; set; }

        #endregion

        #region Navigations
        public ViewStatus ViewStatus { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableViewStatusType { get; set; }
        #endregion
    }
}
