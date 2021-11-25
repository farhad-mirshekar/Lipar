using Lipar.Web.Framework.MVC.Attributes;

namespace Lipar.Web.Models.Organization
{
    public class LoginModel
    {
        [Required("Account.Login.Field.Username.ErrorMessage")]
        public string Username { get; set; }
        [Required("Account.Login.Field.Password.ErrorMessage")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool DisplayCaptcha { get; set; }
        public string Captcha { get; set; }
        public string CaptchaText { get; set; }
        public string ReturnUrl { get; set; }
    }
}
