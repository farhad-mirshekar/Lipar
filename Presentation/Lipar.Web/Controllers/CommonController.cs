using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.General;
using Lipar.Services.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Factories;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Models.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Lipar.Web.Controllers
{
    public class CommonController : BasePublishController
    {
        #region Ctor
        public CommonController(ICommonModelFactory commonModelFactory
                              , IContactUsService contactUsService
                              , ILocaleStringResourceService localeStringResourceService
                              , ILanguageService languageService
                              , IWorkContext workContext
                              , IHttpContextAccessor httpContextAccessor
                              , IStaticCacheManager cacheKeyService)
        {
            _commonModelFactory = commonModelFactory;
            _contactUsService = contactUsService;
            _localeStringResourceService = localeStringResourceService;
            _languageService = languageService;
            _workContext = workContext;
            _httpContextAccessor = httpContextAccessor;
            _cacheKeyService = cacheKeyService;
        }
        #endregion

        #region Fields
        private readonly ICommonModelFactory _commonModelFactory;
        private readonly IContactUsService _contactUsService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly ILanguageService _languageService;
        private readonly IWorkContext _workContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStaticCacheManager _cacheKeyService;
        #endregion

        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            Response.ContentType = "text/html";

            return View();
        }

        public IActionResult ContactUs()
        {
            var model = _commonModelFactory.PrepareContactUsModel(new ContactUsModel());
            return View(model);
        }

        [HttpPost]
        public IActionResult ContactUs(ContactUsModel model)
        {
            model = _commonModelFactory.PrepareContactUsModel(model);

            if (ModelState.IsValid)
            {
                var contactUs = new ContactUs
                {
                    Body = model.Body,
                    ContactUsTypeId = model.ContactUsTypeId,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                };

                //insert contact us
                _contactUsService.Add(contactUs);

                model.State = true;
                model.Result = _localeStringResourceService.GetResource("Web.ContactUs.Success");

                return View(model);
            }


            model.State = true;
            model.Result = _localeStringResourceService.GetResource("Web.ContactUs.Error");

            return View(model);
        }

        public IActionResult SetLanguage(int languageId)
        {
            var returnUrl = Url.RouteUrl("Homepage");

            var language = _languageService.GetById(languageId, true);

            if(language != null && language.ViewStatusId == (int)ViewStatusEnum.Active)
            {
                _localeStringResourceService.ClearCacheLocalStringResources(_workContext.WorkingLanguage.Id);

                _workContext.WorkingLanguage = language;
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserCulture");

            Response.Cookies.Append("UserCulture", CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language.LanguageCulture.Seo)),new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            
            return Redirect(returnUrl);
        }

        public JsonResult Countries()
        {
            var counries = new List<SelectListItem>();
            _commonModelFactory.PrepareCountries(counries);

            return Json(counries);
        }

        public JsonResult Provinces(int? countryId = null)
        {
            var provinces = new List<SelectListItem>();
            _commonModelFactory.PrepareProvinces(provinces , countryId);

            return Json(provinces);
        }

        public JsonResult Cities(int? provinceId = null)
        {
            var cities = new List<SelectListItem>();
            _commonModelFactory.PrepareCities(cities, provinceId);

            return Json(cities);
        }
    }
}
