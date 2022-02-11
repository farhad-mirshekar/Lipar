using System;

namespace Lipar.Web.Areas.Admin.Models.Organization
{
    public class RolePermissionModel
    {
        public Guid RoleId { get; set; }
        public Guid CommandId { get; set; }
        public string CommandName { get; set; }
        public int CommantTypeId { get; set; }
    }
}
