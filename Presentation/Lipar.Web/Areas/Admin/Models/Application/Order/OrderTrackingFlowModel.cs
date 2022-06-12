using System;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class OrderTrackingFlowModel : BaseEntityModel<Guid>
    {
        public Guid OrderTrackingId { get; set; }
        public Guid OrderId { get; set; }
        public Guid LastPositionId { get; set; }
        public int LastToDocStateId { get; set; }
        public string CustomerFullName { get; set; }
        public decimal Price { get; set; }
        public string BankName { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
