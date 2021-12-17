using Lipar.Entities.Domain.Core;
using System;

namespace Lipar.Entities.Domain.Financial
{
    /// <summary>
    /// bank port
    /// </summary>
   public class BankPort :BaseEntity
    {
        #region Fields
        /// <summary>
        /// gets or sets bank id
        /// </summary>
        public int BankId { get; set; }
        /// <summary>
        /// gets or sets merchant id
        /// </summary>
        public string MerchantId { get; set; }
        /// <summary>
        /// gets or sets the merchant key
        /// </summary>
        public string MerchantKey { get; set; }
        /// <summary>
        /// gets or sets username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// gets or sets password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// gets or sets terminal id
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// gets or sets is default
        /// </summary>
        public bool? IsDefault { get; set; }
        /// <summary>
        /// gets or sets enable type id
        /// </summary>
        public int EnabledTypeId { get; set; }
        /// <summary>
        /// gets or sets modified date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        #endregion

        #region Navigations
        public Bank Bank { get; set; }
        public EnabledType EnabledType { get; set; }
        #endregion
    }
}
