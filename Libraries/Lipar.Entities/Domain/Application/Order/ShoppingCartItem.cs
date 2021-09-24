using Lipar.Entities.Domain.Organization;
using System;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// shopping cart item
    /// </summary>
   public class ShoppingCartItem : BaseEntity
    {
        #region Fields
        /// <summary>
        /// gets or sets the product id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// gets or sets the user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// gets or sets the attribute json
        /// </summary>
        public string AttributeJson { get; set; }
        /// <summary>
        /// gets or sets the modified date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        #endregion

        #region Navigations
        public Product Product { get; set; }
        public User User { get; set; }
        #endregion
    }
}
