using Lipar.Entities.Domain.Core;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Financial
{
    /// <summary>
    /// bank
    /// </summary>
   public class Bank : BaseEntity<Guid>
    {
        #region Ctor
        public Bank()
        {
            BankPorts = new HashSet<BankPort>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets or sets the payment uri
        /// </summary>
        public string PaymentUri { get; set; }
        /// <summary>
        /// gets or sets the service uri
        /// </summary>
        public string ServiceUri { get; set; }
        /// <summary>
        /// gets or sets redirect uri
        /// </summary>
        public string RedirectUri { get; set; }
        /// <summary>
        /// gets or sets the transaction cost
        /// </summary>
        public int? TransactionCost { get; set; }
        /// <summary>
        /// gets or sets the enabled type
        /// </summary>
        public int EnabledTypeId { get; set; }
        /// <summary>
        /// gets or sets the modified date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// gets or sets bank type
        /// </summary>
        public int BankTypeId { get; set; }
        #endregion

        #region Navigations
        public EnabledType EnabledType { get; set; }
        public ICollection<BankPort> BankPorts { get; set; }
        #endregion
    }
}
