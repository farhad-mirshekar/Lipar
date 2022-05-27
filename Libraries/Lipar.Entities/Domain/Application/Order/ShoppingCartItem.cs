using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// shopping cart item
    /// </summary>
   public class ShoppingCartItem : BaseEntity<Guid>
    {
        #region Fields
        /// <summary>
        /// gets or sets the product id
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// gets or sets the user id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// gets or sets the shopping cart item cookie
        /// </summary>
        public Guid ShoppingCartItemId { get; set; }
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
