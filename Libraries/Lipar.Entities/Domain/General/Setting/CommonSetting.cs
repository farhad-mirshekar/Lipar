using Lipar.Entities.Configuration;

namespace Lipar.Entities.Domain.General
{
    /// <summary>
    /// common settings
    /// </summary>
   public class CommonSetting : ISettings
    {
        /// <summary>
        /// get or set the show captcha
        /// </summary>
        public bool ShowCaptcha { get; set; }
        /// <summary>
        /// gets or sets the show captcha in login page
        /// </summary>
        public bool ShowCaptchaInLoginPage { get; set; }
        /// <summary>
        /// gets or sets the show captcha in register page
        /// </summary>
        public bool ShowCaptchaInRegisterPage { get; set; }
    }
}
