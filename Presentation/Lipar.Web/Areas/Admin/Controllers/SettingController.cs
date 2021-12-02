using Lipar.Core.Caching;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.General;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.General.Contracts;
using Lipar.Services.Portal;
using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class SettingController : BaseAdminController
    {
        #region Ctor
        public SettingController(ISettingService settingService
                               , ISettingModelFactory settingModelFactory
                               , IStaticCacheManager cacheManager)
        {
            _settingService = settingService;
            _settingModelFactory = settingModelFactory;
            _cacheManager = cacheManager;
        }
        #endregion

        #region Fields
        private readonly ISettingService _settingService;
        private readonly ISettingModelFactory _settingModelFactory;
        private readonly IStaticCacheManager _cacheManager;
        #endregion

        #region Blog Setting Methods
        public IActionResult Blog()
        {
            var model = _settingModelFactory.PrepareBlogSettingModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Blog(BlogSettingModel model)
        {
            var blogSetting = _settingService.LoadSettings<BlogSetting>();
            blogSetting = model.ToSettings(blogSetting);

            _settingService.SaveSetting(blogSetting, x => x.BlogPageSize);

            //remove load blog settings cache
            _cacheManager.Remove(LiparPortalDefaults.Load_Blog_Settings);

            return View();
        }
        #endregion

        #region Order Setting Methods
        public IActionResult Order()
        {
            var model = _settingModelFactory.PrepareOrderSettingModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Order(OrderSettingModel model)
        {
            var orderSetting = _settingService.LoadSettings<OrderSetting>();
            orderSetting = model.ToSettings(orderSetting);

            _settingService.SaveSetting(orderSetting, x => x.ShoppingCartRate);

            //remove load blog settings cache
            //_cacheManager.Remove(LiparPortalDefaults.Load_Blog_Settings);

            return View();
        }
        #endregion

        #region Order Setting Methods
        public IActionResult Common()
        {
            var model = _settingModelFactory.PrepareCommonSettingModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Common(CommonSettingModel model)
        {
            var commonSetting = _settingService.LoadSettings<CommonSetting>();
            commonSetting = model.ToSettings(commonSetting);

            _settingService.SaveSetting(commonSetting, x => x.ShowCaptcha);
            _settingService.SaveSetting(commonSetting, x => x.ShowCaptchaInLoginPage);
            _settingService.SaveSetting(commonSetting, x => x.ShowCaptchaInRegisterPage);

            return View();
        }
        #endregion

        #region Security Setting Methods
        public IActionResult Security()
        {
            var model = _settingModelFactory.PrepareSecuritySettingModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Security(SecuritySettingModel model)
        {
            var securitySetting = _settingService.LoadSettings<SecuritySetting>();
            securitySetting = model.ToSettings(securitySetting);

            _settingService.SaveSetting(securitySetting, x => x.EncryptionKey);
            _settingService.SaveSetting(securitySetting, x => x.PasswordFormatType);

            return View();
        }
        #endregion
    }
}
