using System;
using System.Collections.Generic;

namespace Lipar.ViewModels.Application
{
    public class OrderDetailViewModel
    {
        public OrderDetailViewModel()
        {
            OrderDetailAttributes = new List<OrderDetailAttributeViewModel>();
        }
        public Guid OrderDetailId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int? ProductDiscountTypeId { get; set; }
        public decimal? ProductDiscountPrice { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public Guid? ShippingCostId { get; set; }
        public string ShippingCostName { get; set; }
        public int? ShippingCostPriority { get; set; }
        public Guid? DeliveryDateId { get; set; }
        public string DeliveryDateName { get; set; }
        public int? DeliveryDatePriority { get; set; }
        public int Quantity { get; set; }

        public IEnumerable<OrderDetailAttributeViewModel> OrderDetailAttributes { get; set; }
    }
}
