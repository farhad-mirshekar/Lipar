using Lipar.Core;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Models.Common;
using Lipar.Web.Models.Portal;
using Lipar.Web.Models.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Lipar.Entities.Domain.General;
using Lipar.Services.Core.Contracts;

namespace Lipar.Web.Factories
{
    public class CommonModelFactory : ICommonModelFactory
    {
        #region Ctor
        public CommonModelFactory(IWorkContext workContext
                                , IStaticPageService staticPageService
                                , IDynamicPageService dynamicPageService
                                , IContactUsTypeService contactUsTypeService
                                , ILanguageService languageService
                                , ICountryService countryService
                                , IProvinceService provinceService
                                , ICityService cityService
                                , IGenderService genderService)
        {
            _workContext = workContext;
            _staticPageService = staticPageService;
            _dynamicPageService = dynamicPageService;
            _contactUsTypeService = contactUsTypeService;
            _languageService = languageService;
            _countryService = countryService;
            _provinceService = provinceService;
            _cityService = cityService;
            _genderService = genderService;
        }
        #endregion

        #region Fields
        private readonly IWorkContext _workContext;
        private readonly IStaticPageService _staticPageService;
        private readonly IDynamicPageService _dynamicPageService;
        private readonly IContactUsTypeService _contactUsTypeService;
        private readonly ILanguageService _languageService;
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;
        private readonly ICityService _cityService;
        private readonly IGenderService _genderService;
        #endregion

        #region Methods
        public MenuModel PrepareMenuModel()
        {
            var menuModel = new MenuModel();

            var staticPages = _staticPageService.List(new StaticPageListVM
            {
                IncludeInTopMenu = true,
                ViewStatusId = (int)ViewStatusEnum.Active,
            });

            if(staticPages != null)
            {
                var staticPagesList = staticPages.ToList();
                staticPagesList.ForEach(staticPage =>
                {
                    var staticPageModel = new StaticPageModel();
                    staticPageModel.Name = staticPage.Name;
                    staticPageModel.Title = staticPage.Title;
                    staticPageModel.Id = staticPage.Id;

                    menuModel.StaticPages.Add(staticPageModel);
                });

                menuModel.UserHeaderLink = PrepareHeaderLinksModel();

                var dynamicPages = _dynamicPageService.List(new DynamicPageListVM
                {
                    IncludeInTopMenu = true,
                    //ViewStatusType = ViewStatusType.فعال
                });

                var dynamicPagesList = dynamicPages.ToList();
                dynamicPagesList.ForEach(dynamicPage =>
                {
                    var dynamicPageModel = new DynamicPageModel();
                    dynamicPageModel.Name = dynamicPage.Name;
                    dynamicPageModel.Title = dynamicPage.Title;
                    dynamicPageModel.Id = dynamicPage.Id;

                    menuModel.dynamicPages.Add(dynamicPageModel);
                });
            }

           

            return menuModel;
        }
        public void PrepareContacatUsType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var contactUsTypes = _contactUsTypeService.List();

            foreach (var contactUsType in contactUsTypes)
            {
                items.Add(new SelectListItem
                {
                    Text = contactUsType.Title,
                    Value = contactUsType.Id.ToString(),
                });
            }

            PrepareDefaultItem(items, defaultItemText);
        }
        public ContactUsModel PrepareContactUsModel(ContactUsModel model)
        {
            if (_workContext.CurrentUser != null)
            {
                model.FirstName = _workContext.CurrentUser.FirstName;
                model.LastName = _workContext.CurrentUser.LastName;
                model.Phone = _workContext.CurrentUser.CellPhone;
                model.Email = _workContext.CurrentUser.Email;
            }

            PrepareContacatUsType(model.AvailableContactUsType);

            return model;
        }
        public LanguageSelectorModel PrepareLanguageSelectorModel()
        {
            var availableLanguages = _languageService.List(new LanguageListVM { })
                .Select(l => new LanguageModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    UniqueSeoCode = l.UniqueSeoCode,
                    CreationDate = l.CreationDate
                }).ToList();

            var model = new LanguageSelectorModel
            {
                AvailableLanguages = availableLanguages,
                CurrentLanguageId = _workContext.WorkingLanguage.Id,
            };

            return model;
        }

        public void PrepareCountries(IList<SelectListItem> items, string defaultItemText = null)
        {
            var countries = _countryService.List();

            foreach (var country in countries)
            {
                items.Add(new SelectListItem
                {
                    Text = country.Title,
                    Value = country.Id.ToString(),
                });
            }

            PrepareDefaultItem(items, defaultItemText);
        }


        public void PrepareProvinces(IList<SelectListItem> items,int? countryId, string defaultItemText = null)
        {
            var provinces = _provinceService.List(new ProvinceListVM
            {
                CountryId = countryId,
            });

            foreach (var province in provinces)
            {
                items.Add(new SelectListItem
                {
                    Text = province.Title,
                    Value = province.Id.ToString(),
                });
            }

            PrepareDefaultItem(items, defaultItemText);
        }


        public void PrepareCities(IList<SelectListItem> items,int? provinceId = null, string defaultItemText = null)
        {
            var cities = _cityService.List(new CityListVM
            {
                ProvinceId = provinceId,
            });

            foreach (var city in cities)
            {
                items.Add(new SelectListItem
                {
                    Text = city.Title,
                    Value = city.Id.ToString(),
                });
            }

            PrepareDefaultItem(items, defaultItemText);
        }

        #endregion

        #region Utilities
        protected UserHeaderLinkModel PrepareHeaderLinksModel()
        {
            var user = _workContext.CurrentUser;

            var model = new UserHeaderLinkModel();

            if (user != null)
            {
                model.UserInfo = $"{user.FirstName} {user.LastName}";
                model.IsAuthenticated = true;
                model.UserTypeId = user.UserTypeId;
                model.GenderId = user.GenderId;
            }

            return model;
        }
        protected void PrepareDefaultItem(IList<SelectListItem> items, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (string.IsNullOrWhiteSpace(defaultItemText))
            {
                defaultItemText = "انتخاب نمایید";
            }

            const string value = "";
            items.Insert(0, new SelectListItem { Value = value, Text = defaultItemText });
        }

        public void PrepareGenderType(IList<SelectListItem> items, string defaultItemText = null)
        {
            var genders = _genderService.GetGenders(); ;

            foreach (var gender in genders)
            {
                items.Add(new SelectListItem
                {
                    Text = gender.Title,
                    Value = gender.Id.ToString(),
                });
            }

            PrepareDefaultItem(items, defaultItemText);
        }
        #endregion
    }
}
