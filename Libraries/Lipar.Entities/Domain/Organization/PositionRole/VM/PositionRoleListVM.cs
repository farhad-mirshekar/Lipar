using System;

namespace Lipar.Entities.Domain.Organization
{
    public class PositionRoleListVM : BaseListVM
    {
        public Guid? RoleId { get; set; }
        public Guid? PositionId { get; set; }
    }
}
