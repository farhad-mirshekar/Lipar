using System;

namespace Lipar.Entities.Domain.Organization
{
    public class PositionRole : BaseEntity<Guid>
    {
        public Position Position { get; set; }
        public Guid PositionId { get; set; }

        public Role Role { get; set; }
        public Guid RoleId { get; set; }
    }
}
