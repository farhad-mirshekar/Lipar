namespace Lipar.Web.Models.Organization
{
    public class PasswordRecoveryModel
    {
        public string Email { get; set; }
        public string Captcha { get; set; }
        public bool ShowCaptcha { get; set; }
        public bool ShowCaptchaInPasswordRecoveryPage { get; set; }
        public string Result { get; set; }
    }
}
