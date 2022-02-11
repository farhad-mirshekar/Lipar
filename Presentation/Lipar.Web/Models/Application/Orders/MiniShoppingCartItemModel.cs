using Lipar.Web.Models.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.Application
{
    public class MiniShoppingCartItemModel : BaseEntityModel<Guid>
    {
        public MiniShoppingCartItemModel()
        {
            AvailableUserAddress = new List<UserAddressModel>();
            DeliveryDate = new DeliveryDateModel();
            ShippingCost = new ShippingCostModel();
            AddUserAddressModel = new UserAddressModel();
        }

        /// <summary>
        /// قیمت محصول نهایی
        /// </summary>
        public decimal CartAmount { get; set; }
        /// <summary>
        /// مبلغ کل تخفیف ها
        /// </summary>
        public decimal? CartDiscountAmount { get; set; }
        /// <summary>
        /// زمان ارسال محصول
        /// </summary>
        public DeliveryDateModel DeliveryDate { get; set; }
        /// <summary>
        /// هزینه ارسال محصول
        /// </summary>
        public ShippingCostModel ShippingCost { get; set; }
        /// <summary>
        /// مبلغ کل محصول
        /// </summary>
        public decimal AmountProducts { get; set; }
        public Guid ShoppingCartItemId { get; set; }
        
        /// <summary>
        /// لیست آدرس های کاربر
        /// </summary>
        public IList<UserAddressModel> AvailableUserAddress { get; set; }
        public UserAddressModel AddUserAddressModel { get; set; }
    }
}
