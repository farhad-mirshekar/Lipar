using Lipar.Web.Areas.Admin.Models.Organization.User;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Organization
{
    public class UserModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public UserModel()
        {
            AvailablePositionRole = new List<PositionRoleModel>();
            AvailableRoles = new List<RolesModel>();
        }
        #endregion

        #region Fields
        public string Username { get; set; }
        public string NationalCode { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserTypeId { get; set; }
        public string CellPhone { get; set; }
        public IEnumerable<RolesModel> AvailableRoles { get; set; }
        public IEnumerable<PositionRoleModel> AvailablePositionRole { get; set; }
        #endregion
    }
}
