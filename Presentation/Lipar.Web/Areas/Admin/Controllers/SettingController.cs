using Lipar.Entities.Domain.Portal;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class SettingController : BaseAdminController
    {
        #region Fields
        private readonly ISettingService _settingService;
        private readonly ISettingModelFactory _settingModelFactory;
        #endregion

        #region Ctor
        public SettingController(ISettingService settingService
                               , ISettingModelFactory settingModelFactory)
        {
            _settingService = settingService;
            _settingModelFactory = settingModelFactory;
        }
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
            return View();
        }
        #endregion
    }
}
