using System;

namespace Lipar.Entities.Domain.General
{
   public class QueuedEmail : BaseEntity
    {
        /// <summary>
        /// Gets or sets the priority
        /// </summary>
        public int PriorityId { get; set; }

        /// <summary>
        /// Gets or sets the From property (email address)
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the FromName property
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the To property (email address)
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the ToName property
        /// </summary>
        public string ToName { get; set; }
        /// <summary>
        /// Gets or sets the subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Gets or sets the used email account identifier
        /// </summary>
        public int EmailAccountId { get; set; }

        /// <summary>
        /// gets or sets the time send
        /// </summary>
        public DateTime? TimeSend { get; set; }

        #region Navigations
        public EmailAccount EmailAccount { get; set; }
        #endregion
    }
}
