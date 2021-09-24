using Lipar.Entities.Configuration;

namespace Lipar.Services.Security
{
    public class SecuritySettings : ISettings
    {
        public string EncryptionKey { get; set; }
    }
}
