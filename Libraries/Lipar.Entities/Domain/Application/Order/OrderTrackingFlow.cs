using Lipar.Entities.Domain.Organization;
using System;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// order tracking flow
    /// </summary>
    public class OrderTrackingFlow : BaseEntity<Guid>
    {
        #region Fields
        /// <summary>
        /// gets or sets the order tracking id
        /// </summary>
        public Guid OrderTrackingId { get; set; }

        /// <summary>
        /// gets or sets the from position
        /// </summary>
        public Guid? FromPositionId { get; set; }

        /// <summary>
        /// gets or sets the from doc state
        /// </summary>
        public int FromDocStateId { get; set; }

        /// <summary>
        /// gets or sets the to position
        /// </summary>
        public Guid? ToPositionId { get; set; }

        /// <summary>
        /// gets or sets the to doc state
        /// </summary>
        public int ToDocStateId { get; set; }

        /// <summary>
        /// gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// gets or sets the action date
        /// </summary>
        public DateTime? ActionDate { get; set; }
        #endregion

        #region Navigations
        public OrderTracking OrderTracking { get; set; }
        public Position FromPosition { get; set; }
        public Position ToPosition { get; set; }
        public OrderTrackingDocState FromDocState { get; set; }
        public OrderTrackingDocState ToDocState { get; set; }
        #endregion
    }
}
