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
        public int FromDocStateId { get; set; }
        public string FromDocStateTitle { get; set; }
        public Guid? FromPositionId { get; set; }
        public string FromPositionFullName { get; set; }
        public int ToDocStateId { get; set; }
        public string ToDocStateTitle { get; set; }
        public Guid? ToPositionId { get; set; }
        public string ToPositionFullName { get; set; }
        public string Description { get; set; }
        public DateTime? ActionDate { get; set; }
    }
}
