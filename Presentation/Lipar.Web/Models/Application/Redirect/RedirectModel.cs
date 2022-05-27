using System;

namespace Lipar.Web.Models.Application
{
    /// <summary>
    /// redirect model
    /// </summary>
    public class RedirectModel
    {
        /// <summary>
        /// gets or sets payment status
        /// </summary>
        public bool PaymentStatus { get; set; }

        /// <summary>
        /// gets or sets the order id
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// gets or sets the message
        /// </summary>
        public string Message { get; set; }
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
        public string ReservationNumber { get; set; }

        public Decimal? Amount { get; set; }
    }
}
