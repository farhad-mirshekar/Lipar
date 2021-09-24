using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Organization
{
    public class RoleModel : BaseEntityModel
    {
        #region Ctor
        public RoleModel()
        {
            AvailableCommands = new List<CommandModel>();
            AvailableRolePermission = new List<RolePermissionModel>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public IEnumerable<CommandModel> AvailableCommands { get; set; }
        public IEnumerable<RolePermissionModel> AvailableRolePermission { get; set; }

        #endregion
    }
}
