using System;

namespace Lipar.Web.Areas.Admin.Models.Organization
{
    public class PositionRoleModel : BaseEntityModel<Guid>
    {
        public Guid RoleId { get; set; }
        public Guid PositionId { get; set; }
    }
}
