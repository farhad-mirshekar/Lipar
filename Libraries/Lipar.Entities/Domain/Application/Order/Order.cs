using Lipar.Entities.Domain.Financial;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// order
    /// </summary>
    public class Order : BaseEntity<Guid>
    {
        #region Ctor
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            OrderPaymentStatuses = new HashSet<OrderPaymentStatus>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the user id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// gets or sets the user address id
        /// </summary>
        public Guid UserAddressId { get; set; }

        /// <summary>
        /// gets or sets the bank port
        /// </summary>
        public Guid BankPortId { get; set; }

        /// <summary>
        /// gets or sets the shipping cart rate
        /// </summary>
        public decimal? ShoppingCartRate { get; set; }

        /// <summary>
        /// gets or sets price
        /// </summary>
        public decimal Price { get; set; }
        #endregion

        #region Navigations
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<OrderPaymentStatus> OrderPaymentStatuses { get; set; }
        public BankPort BankPort { get; set; }
        public User User { get; set; }
        public UserAddress UserAddress { get; set; }
        #endregion
    }
}
