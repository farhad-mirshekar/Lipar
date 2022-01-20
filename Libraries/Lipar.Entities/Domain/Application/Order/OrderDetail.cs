namespace Lipar.Entities.Domain.Application
{
    public class OrderDetail : BaseEntity
    {
        #region Fields
        /// <summary>
        /// gets or sets the order id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// gets or sets the product json
        /// </summary>
        public string ProductJson { get; set; }
        /// <summary>
        /// gets or sets the user json
        /// </summary>
        public string UserJson { get; set; }
        /// <summary>
        /// gets or sets the attribute product json
        /// </summary>
        public string AttributeProductJson { get; set; }
        /// <summary>
        /// gets or sets the shopping cart item json
        /// </summary>
        public string ShoppingCartItemJson { get; set; }
        /// <summary>
        /// gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }
        #endregion

        #region Navigations
        public Order Order { get; set; }
        #endregion
    }
}
