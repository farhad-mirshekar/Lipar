using Lipar.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lipar.Web.Framework.Models;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Organization
{
    public class PositionSearchModel : BaseSearchModel
    {
        #region Ctor
        public PositionSearchModel()
        {
            AvailableDepartments = new List<SelectListItem>();
            AvailableEnabledType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public int? DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public int EnabledTypeId { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableDepartments { get; set; }
        public IList<SelectListItem> AvailableEnabledType { get; set; }
        #endregion
    }
}
