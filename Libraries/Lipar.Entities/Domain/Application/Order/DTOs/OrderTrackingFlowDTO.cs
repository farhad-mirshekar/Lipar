using System;

namespace Lipar.Entities.Domain.Application.DTOs
{
    public class OrderTrackingFlowDTO
    {
        public Guid Id { get; set; }
        public Guid OrderTrackingId { get; set; }
        public Guid OrderId { get; set; }
        public Guid LastPositionId { get; set; }
        public int LastToDocStateId { get; set; }
        public string LastDocStateTitle { get; set; }
        public string CustomerFullName { get; set; }
        public decimal Price { get; set; }
        public string BankName { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
