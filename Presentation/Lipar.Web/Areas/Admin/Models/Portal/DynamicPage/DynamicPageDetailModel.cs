﻿using Lipar.Entities;
using Lipar.Entities.Domain.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class DynamicPageDetailModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public DynamicPageDetailModel()
        {
            AvailableViewStatusType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public int Priority { get; set; }
        public int ViewStatusId { get; set; }
        public Guid DynamicPageId { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableViewStatusType { get; set; }
        #endregion

        #region Navigations
        public ViewStatus ViewStatus { get; set; }
        #endregion
    }
}
