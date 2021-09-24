using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
    public class Command : BaseEntity
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
        public DateTime? RemoveDate { get; set; }
        public int? RemoverId { get; set; }

        //navigations
        public int? ParentId { get; set; }
        public Command Parent { get; set; }
        public ICollection<Command> Children { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public CommandType CommandType { get; set; }
    }
}
