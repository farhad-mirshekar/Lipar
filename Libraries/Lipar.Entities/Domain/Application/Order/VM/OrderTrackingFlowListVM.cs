using System;

namespace Lipar.Entities.Domain.Application
{
    public class OrderTrackingFlowListVM : BaseListVM
    {
        public Guid? ToPositionId { get; set; }
        public Guid? OrderTrackingId { get; set; }
        public bool? ActionState { get; set; }
    }
}
