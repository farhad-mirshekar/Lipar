using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    public class OrderTrackingDocState : BaseEntity<int>
    {
        #region Ctor
        public OrderTrackingDocState()
        {
            FromDocStates = new HashSet<OrderTrackingFlow>();
            ToDocStates = new HashSet<OrderTrackingFlow>();
        }
        #endregion

        #region Fields
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<OrderTrackingFlow> FromDocStates { get; set; }
        public ICollection<OrderTrackingFlow> ToDocStates { get; set; }
        #endregion
    }
}
