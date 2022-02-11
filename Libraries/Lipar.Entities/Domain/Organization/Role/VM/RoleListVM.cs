using System;

namespace Lipar.Entities.Domain.Organization
{
    public class RoleListVM : BaseListVM
    {
        public Guid? CenterId { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? UserId { get; set; }
        public string Name { get; set; }
    }
}
