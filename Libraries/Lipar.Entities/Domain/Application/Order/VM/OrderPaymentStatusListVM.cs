using System;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// order payment status list vm
    /// </summary>
    public class OrderPaymentStatusListVM:BaseListVM
    {
        public int? OrderPaymentStatusTypeId { get; set; }
        public Guid? UserId { get; set; }
    }
}
