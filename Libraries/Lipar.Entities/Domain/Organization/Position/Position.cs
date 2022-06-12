using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Core;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
    public class Position : BaseEntity<Guid>
    {
        #region Ctor
        public Position()
        {
            FromOrderTrackingFlows = new HashSet<OrderTrackingFlow>();
            ToOrderTrackingFlows = new HashSet<OrderTrackingFlow>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the position type
        /// </summary>
        public int PositionTypeId { get; set; }
        /// <summary>
        /// gets or sets the enable type
        /// </summary>
        public int EnabledTypeId { get; set; }
        public bool Default { get; set; }
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid CenterId { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations

        public User User { get; set; }
        public Department Department { get; set; }
        public Center Center { get; set; }
        public ICollection<PositionRole> PositionRoles { get; set; }
        public PositionType PositionType { get; set; }
        public EnabledType EnabledType { get; set; }
        public ICollection<OrderTrackingFlow> FromOrderTrackingFlows { get; set; }
        public ICollection<OrderTrackingFlow> ToOrderTrackingFlows { get; set; }
        #endregion
    }
}
