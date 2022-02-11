using System;

namespace Lipar.Entities.Domain.Application
{
    public class ShippingCartItemListVM : BaseListVM
    {
        public Guid? ProductId { get; set; }
        public Guid? UserId { get; set; }
    }
}
