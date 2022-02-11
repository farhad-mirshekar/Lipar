using System;

namespace Lipar.Entities.Domain.Organization
{
    public class RolePermission : BaseEntity<Guid>
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Guid CommandId { get; set; }
        public Command Command { get; set; }
    }
}
