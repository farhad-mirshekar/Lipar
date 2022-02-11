using Lipar.Entities;
using Lipar.Entities.Domain.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ShippingCostModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public ShippingCostModel()
        {
            AvailableEnabledType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int EnabledTypeId { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableEnabledType { get; set; }
        #endregion

        #region Navigations
        public EnabledType EnabledType { get; set; }
        #endregion
    }
}
