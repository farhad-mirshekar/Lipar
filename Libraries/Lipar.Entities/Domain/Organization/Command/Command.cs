using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
    public class Command : BaseEntity<Guid>
    {
        #region Ctor
        public Command()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
        #endregion
        public string Name { get; set; }
        public string SystemName { get; set; }
        public int CommandTypeId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }

        //navigations
        public Command Parent { get; set; }
        public ICollection<Command> Children { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public CommandType CommandType { get; set; }
    }
}
