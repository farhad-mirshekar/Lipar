using System;
using System.Collections.Generic;
using System.Text;

namespace Lipar.ViewModels.Application.Order
{
    public class OrderTrackingFlowViewModel
    {
        public Guid Id { get; set; }
        public Guid OrderTrackingId { get; set; }
        public int FromDocStateId { get; set; }
        public string FromDocStateTitle { get; set; }
        public int ToDocStateId { get; set; }
        public string ToDocStateTitle { get; set; }
        public DateTime? ActionDate { get; set; }

    }
}
