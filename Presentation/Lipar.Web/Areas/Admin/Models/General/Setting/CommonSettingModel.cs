using Lipar.Web.Framework.Models;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class CommonSettingModel : BaseEntityModel, ISettingsModel
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
