using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;


namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class SettingModelFactory : ISettingModelFactory
    {
        #region Fields
        private readonly ISettingService _settingService;
        #endregion

        #region Ctor
        public SettingModelFactory(ISettingService settingService)
        {
            _settingService = settingService;
        }
        #endregion
        public BlogSettingModel PrepareBlogSettingModel()
        {
            var blogSettings = _settingService.LoadSettings<BlogSetting>();

            var blogSettingModel = blogSettings.ToSettingsModel<BlogSettingModel>();

            return blogSettingModel;
        }

        public OrderSettingModel PrepareOrderSettingModel()
        {
            var orderSetting = _settingService.LoadSettings<OrderSetting>();

            var orderSettingModel = orderSetting.ToSettingsModel<OrderSettingModel>();

            return orderSettingModel;
        }
    }
}
