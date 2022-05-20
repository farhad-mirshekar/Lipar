using System;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// order payment status
    /// </summary>
    public class OrderPaymentStatus : BaseEntity<Guid>
    {

        #region Fields

        /// <summary>
        /// gets or sets the order
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// gets or sets the shopping cart item
        /// </summary>
        public Guid? ShoppingCartItemId { get; set; }

        /// <summary>
        /// gets or sets Order Payment Status Type
        /// </summary>
        public int OrderPaymentStatusTypeId { get; set; }

        #endregion

        #region Navigations
        public Order Order { get; set; }
        public ShoppingCartItem shoppingCartItem { get; set; }
        #endregion
    }
}
