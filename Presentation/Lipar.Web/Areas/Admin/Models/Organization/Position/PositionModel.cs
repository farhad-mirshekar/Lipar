using Lipar.Entities.Domain.Organization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Organization
{
    public class PositionModel : BaseEntityModel
    {
        #region Ctor
        public PositionModel()
        {
            AvailablePositionType = new List<SelectListItem>();
            AvailableEnableType = new List<SelectListItem>();
            AvailableRoles = new List<RoleModel>();
            AvailablePositionRole = new List<PositionRoleModel>();
            AvailableDepartments = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public int PositionTypeId { get; set; }
        public string PositionTypeTitle { get; set; }
        public int EnabledTypeId { get; set; }

        #region User Fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        #endregion

        public int DepartmentId { get; set; }

        public IEnumerable<RoleModel> AvailableRoles { get; set; }
        public IEnumerable<PositionRoleModel> AvailablePositionRole { get; set; }

        #endregion

        #region Select
        public IList<SelectListItem> AvailablePositionType { get; set; }
        public IList<SelectListItem> AvailableEnableType { get; set; }
        public IList<SelectListItem> AvailableDepartments { get; set; }

        #endregion

    }
}
