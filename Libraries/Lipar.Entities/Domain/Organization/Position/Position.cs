using Lipar.Entities.Domain.Core;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
    public class Position : BaseEntity
    {
        /// <summary>
        /// gets or sets the position type
        /// </summary>
        public int PositionTypeId { get; set; }
        /// <summary>
        /// gets or sets the enable type
        /// </summary>
        public int EnabledTypeId { get; set; }
        public bool Default { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public int CenterId { get; set; }

        //navigations
        public User User { get; set; }
        public Department Department { get; set; }
        public Center Center { get; set; }
        public ICollection<PositionRole> PositionRoles { get; set; }
        public PositionType PositionType { get; set; }
        public EnabledType EnabledType { get; set; }
    }
}
