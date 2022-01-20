using Lipar.Entities.Domain.Financial;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// order
    /// </summary>
    public class Order : BaseEntity
    {
        #region Ctor
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// gets or sets the user address id
        /// </summary>
        public int UserAddressId { get; set; }
        /// <summary>
        /// gets or sets the bank port
        /// </summary>
        public int BankPortId { get; set; }
        /// <summary>
        /// gets or sets price
        /// </summary>
        public decimal Price { get; set; }
        #endregion

        #region Navigations
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public BankPort BankPort { get; set; }
        #endregion
    }
}
