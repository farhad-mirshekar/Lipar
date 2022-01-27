using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    /// <summary>
    /// email account
    /// </summary>
   public class EmailAccount : BaseEntity
    {
        #region Ctor
        public EmailAccount()
        {
            MessageTemplates = new HashSet<MessageTemplate>();
            QueuedEmails = new HashSet<QueuedEmail>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets or sets the email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// gets or sets the host
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// gets or sets the username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// gets or sets the password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// gets or sets the port
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// gets or sets the enable ssl
        /// </summary>
        public bool EnableSsl { get; set; }
        #endregion

        #region Navigtions
        public ICollection<MessageTemplate> MessageTemplates { get; set; }
        public ICollection<QueuedEmail> QueuedEmails { get; set; }
        #endregion
    }
}
