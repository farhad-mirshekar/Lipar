using Lipar.Entities.Domain.Application;
using Lipar.Web.Models.General;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.Application
{
    public class ShoppingCartItemModel : BaseEntityModel
    {
        public ShoppingCartItemModel()
        {
            MediaModel = new MediaModel();
            ProductAttributeValues = new List<ProductAttributeValue>();
            DeliveryDate = new DeliveryDateModel();
            ShippingCost = new ShippingCostModel();
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public Guid ShoppingCartItemId { get; set; }
        public MediaModel MediaModel { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal? ProductDiscount { get; set; }
        public int? ProductDiscountTypeId { get; set; }
        public DeliveryDateModel DeliveryDate { get; set; }
        public ShippingCostModel ShippingCost { get; set; }
        public List<ProductAttributeValue> ProductAttributeValues { get; set; }
    }
}
