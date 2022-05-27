using Lipar.Entities.Domain.Organization;
using Lipar.Services.General.Contracts;
using Lipar.Web.Models.Organization;

namespace Lipar.Web.Factories.Organization
{
    public class UserModelFactory : IUserModelFactory
    {
        #region Ctor
        public UserModelFactory(ISettingService settingService
                              , ICommonModelFactory commonModelFactory)
        {
            _settingService = settingService;
            _commonModelFactory = commonModelFactory;
        }
        #endregion

        #region Fields
        private readonly ISettingService _settingService;
        private readonly ICommonModelFactory _commonModelFactory;
        #endregion
        public PasswordRecoveryModel PreparePasswordRecoveryModel(PasswordRecoveryModel model)
        {
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptcha")?.Value, out bool showCaptcha);
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptchaInPasswordRecoveryPage")?.Value, out bool showCaptchaInPasswordRecoveryPage);

            model.ShowCaptcha = showCaptcha;
            model.ShowCaptchaInPasswordRecoveryPage = showCaptchaInPasswordRecoveryPage;

            return model;

        }

        public LoginModel PrepareLoginModel(LoginModel model)
        {
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptcha")?.Value, out bool showCaptcha);
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptchaInLoginPage")?.Value, out bool ShowCaptchaInLoginPage);

            model.ShowCaptcha = showCaptcha;
            model.ShowCaptchaInLoginPage = ShowCaptchaInLoginPage;

            return model;
        }

        public UserAddressModel PrepareUserAddressModel(UserAddressModel model , UserAddress userAddress)
        {
            if(!(userAddress is null))
            {
                model.Address = userAddress.Address;
                model.UserId = userAddress.UserId;
                model.ProvinceId = userAddress.ProvinceId;
                model.CreationDate = userAddress.CreationDate;
                model.CityId = userAddress.CityId;
                model.CountryId = userAddress.CountryId;
                model.Id = userAddress.Id;
                model.PostalCode = userAddress.PostalCode;
            }

            //prepare countries
            _commonModelFactory.PrepareCountries(model.AvailableCountries);

            return model;
        }

        public RegisterModel PrepareRegisterModel()
        {
            throw new System.NotImplementedException();
        }
    }
}
