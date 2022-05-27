using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    public class OrderDetail : BaseEntity<Guid>
    {
        #region Ctor
        public OrderDetail()
        {
            OrderDetailAttributes = new HashSet<OrderDetailAttribute>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the order id
        /// </summary>
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
        /// <summary>
        /// gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }
        #endregion

        #region Navigations
        public Order Order { get; set; }
        public ICollection<OrderDetailAttribute> OrderDetailAttributes { get; set; }
        #endregion
    }
}
