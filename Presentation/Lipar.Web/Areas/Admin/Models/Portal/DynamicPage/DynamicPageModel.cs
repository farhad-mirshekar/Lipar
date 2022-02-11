using Lipar.Entities;
using Lipar.Entities.Domain.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class DynamicPageModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public DynamicPageModel()
        {
            AvailableViewStatusType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public int ViewStatusId { get; set; }
        #endregion

        #region Navigations
        public ViewStatus ViewStatus { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableViewStatusType { get; set; }
        #endregion

        #region Utilities
        public int ApprovedDynamicPageDetail { get; set; }
        public int NotApprovedDynamicPageDetail { get; set; }
        #endregion
    }
}
