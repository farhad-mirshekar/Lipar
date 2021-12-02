using Lipar.Entities.Configuration;

namespace Lipar.Entities.Domain.General
{
    /// <summary>
    /// security setting
    /// </summary>
    public class SecuritySetting : ISettings
    {
        /// <summary>
        /// gets or sets the encryption key
        /// </summary>
        public string EncryptionKey { get; set; }
        /// <summary>
        /// gets or sets the password format type
        /// </summary>
        public int PasswordFormatType { get; set; }
    }
}
