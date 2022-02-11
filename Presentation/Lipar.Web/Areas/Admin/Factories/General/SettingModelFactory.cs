using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.General;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using System;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class SettingModelFactory : ISettingModelFactory
    {
        #region Fields
        private readonly ISettingService _settingService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Ctor
        public SettingModelFactory(ISettingService settingService
                                 , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _settingService = settingService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion
        public BlogSettingModel PrepareBlogSettingModel()
        {
            var blogSettings = _settingService.LoadSettings<BlogSetting>();

            var blogSettingModel = blogSettings.ToSettingsModel<BlogSettingModel, Guid>();

            return blogSettingModel;
        }

        public OrderSettingModel PrepareOrderSettingModel()
        {
            var orderSetting = _settingService.LoadSettings<OrderSetting>();

            var orderSettingModel = orderSetting.ToSettingsModel<OrderSettingModel, Guid>();

            return orderSettingModel;
        }

        public CommonSettingModel PrepareCommonSettingModel()
        {
            var commonSetting = _settingService.LoadSettings<CommonSetting>();

            var commonSettingModel = commonSetting.ToSettingsModel<CommonSettingModel,Guid>();

            return commonSettingModel;
        }

        public SecuritySettingModel PrepareSecuritySettingModel()
        {
            var securitySetting = _settingService.LoadSettings<SecuritySetting>();

            var securitySettingModel = securitySetting.ToSettingsModel<SecuritySettingModel, Guid>();

            _baseAdminModelFactory.PreparePasswordFormatType(securitySettingModel.AvailablePasswordFormatTypes);

            return securitySettingModel;
        }
    }
}
