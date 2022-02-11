using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
    public class Role : BaseEntity<Guid>
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
        public string Name { get; set; }
        public Guid CenterId { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }

        #region Navigations
        public Center Center { get; set; }
        public ICollection<PositionRole> PositionRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
