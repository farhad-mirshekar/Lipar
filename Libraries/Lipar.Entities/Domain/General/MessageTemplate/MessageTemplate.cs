using System;

namespace Lipar.Entities.Domain.General
{
   public class MessageTemplate : BaseEntity<Guid>
    {
        #region Fields
        /// <summary>
        /// gets or sets the name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets or sets the subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// gets or sets the template
        /// </summary>
        public string Template { get; set; }
        /// <summary>
        /// gets or sets the is active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// gets or sets the email address
        /// </summary>
        public Guid EmailAccountId { get; set; }
        #endregion

        #region Navigations
        public EmailAccount EmailAccount { get; set; }
        #endregion
    }
}
