using Lipar.Entities.Domain.Financial;
using Lipar.Entities.Domain.Organization;
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

        /// <summary>
        /// gets or sets user
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// gets or sets modified date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// gets or sets bank port
        /// </summary>
        public Guid? BankPortId { get; set; }

        /// <summary>
        /// gets or sets the user address
        /// </summary>
        public Guid? UserAddressId { get; set; }

        /// <summary>
        /// gets or sets the token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// gets or sets the sign data
        /// </summary>
        public string SignData { get; set; }

        /// <summary>
        /// gets or sets the response status
        /// </summary>
        public int? ResponseStatus { get; set; }

        /// <summary>
        /// gets or sets the response message
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// gets or sets the RetrivalRefNo
        /// </summary>
        public string RetrivalRefNo { get; set; }

        /// <summary>
        /// gets or sets the SystemTraceNo
        /// </summary>
        public string SystemTraceNo { get; set; }

        /// <summary>
        /// gets or sets the ReservationNumber
        /// </summary>
        public string ReservationNumber{ get; set; }

        #endregion

        #region Navigations
        public Order Order { get; set; }
        public BankPort BankPort { get; set; }
        public User User { get; set; }
        #endregion
    }
}
