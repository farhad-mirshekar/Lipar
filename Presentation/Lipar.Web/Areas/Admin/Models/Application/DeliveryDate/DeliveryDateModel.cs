using Lipar.Entities;
using Lipar.Entities.Domain.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class DeliveryDateModel : BaseEntityModel
    {
        #region Ctor
        public DeliveryDateModel()
        {
            AvailableEnabledType = new List<SelectListItem>();
        }
        #endregion


        #region Fields
        public string Name { get; set; }
        public string Description { get; set; }
        public int EnabledTypeId { get; set; }
        public int Priority { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableEnabledType { get; set; }
        #endregion

        #region Navigations
        public EnabledType EnabledType { get; set; }
        #endregion
    }
}
