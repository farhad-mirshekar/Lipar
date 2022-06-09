using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Dashboard.Factories
{
    public class CommonModelFactory : ICommonModelFactory
    {
        #region Ctor
        public CommonModelFactory(ICountryService countryService
                                , IProvinceService provinceService
                                , ICityService cityService
                                , ILocaleStringResourceService localeStringResourceService)
        {
            _countryService = countryService;
            _provinceService = provinceService;
            _cityService = cityService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Fields
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;
        private readonly ICityService _cityService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Methods

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


        public void PrepareProvinces(IList<SelectListItem> items, int? countryId, string defaultItemText = null)
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


        public void PrepareCities(IList<SelectListItem> items, int? provinceId = null, string defaultItemText = null)
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
        protected void PrepareDefaultItem(IList<SelectListItem> items, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (string.IsNullOrWhiteSpace(defaultItemText))
            {
                defaultItemText = _localeStringResourceService.GetResource("Admin.Dropdown.SelectItem.Text");
            }

            const string value = "";
            items.Insert(0, new SelectListItem { Value = value, Text = defaultItemText });
        }
        #endregion
    }
}
