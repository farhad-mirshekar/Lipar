namespace Lipar.Web.Areas.Admin.Models.Organization
{
    public class RolePermissionModel
    {
        public int RoleId { get; set; }
        public int CommandId { get; set; }
        public string CommandName { get; set; }
        public int CommantTypeId { get; set; }
    }
}
