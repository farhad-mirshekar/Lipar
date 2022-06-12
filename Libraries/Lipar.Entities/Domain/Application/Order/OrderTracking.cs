using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// order tracking
    /// </summary>
    public class OrderTracking : BaseEntity<Guid>
    {
        #region Ctor
        public OrderTracking()
        {
            OrderTrackingFlows = new HashSet<OrderTrackingFlow>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the order id
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// gets or sets the order number
        /// </summary>
        public string OrderNumber { get; set; }
        #endregion

        #region Navigations
        public Order Order { get; set; }
        public ICollection<OrderTrackingFlow> OrderTrackingFlows { get; set; }
        #endregion
    }
}
