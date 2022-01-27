using Lipar.Services.General.Contracts;
using Lipar.Web.Models.Organization;

namespace Lipar.Web.Factories.Organization
{
    public class UserModelFactory : IUserModelFactory
    {
        #region Ctor
        public UserModelFactory(ISettingService settingService)
        {
            _settingService = settingService;
        }
        #endregion

        #region Fields
        private readonly ISettingService _settingService;
        #endregion
        public PasswordRecoveryModel PreparePasswordRecoveryModel(PasswordRecoveryModel model)
        {
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptcha").Value, out bool showCaptcha);
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptchaInPasswordRecoveryPage").Value, out bool showCaptchaInPasswordRecoveryPage);

            model.ShowCaptcha = showCaptcha;
            model.ShowCaptchaInPasswordRecoveryPage = showCaptchaInPasswordRecoveryPage;

            return model;

        }
    }
}
