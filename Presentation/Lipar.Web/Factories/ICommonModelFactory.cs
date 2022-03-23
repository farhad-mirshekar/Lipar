using Lipar.Entities.Domain.General;
using Lipar.Web.Models.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Factories
{
    public interface ICommonModelFactory
    {
        /// <summary>
        /// prepare menu main
        /// </summary>
        /// <returns></returns>
        MenuModel PrepareMenuModel();
        /// <summary>
        /// prepare drop down for contact us type
        /// </summary>
        /// <param name="items"></param>
        /// <param name="defaultItemText"></param>
        void PrepareContacatUsType(IList<SelectListItem> items, string defaultItemText = null);
        /// <summary>
        /// prepare contact us model for create
        /// </summary>
        /// <param name="model"></param>
        /// <param name="contactUs"></param>
        /// <returns></returns>
        ContactUsModel PrepareContactUsModel(ContactUsModel model);

        /// <summary>
        /// prepare language selector model
        /// </summary>
        /// <returns></returns>
        LanguageSelectorModel PrepareLanguageSelectorModel();


        /// <summary>
        /// prepare list countries
        /// </summary>
        /// <param name="items"></param>
        /// <param name="defaultItemText"></param>
        void PrepareCountries(IList<SelectListItem> items, string defaultItemText = null);

        /// <summary>
        /// prepare list provinces
        /// </summary>
        /// <param name="items"></param>
        /// <param name="countryId">countryid</param>
        /// <param name="defaultItemText"></param>
        void PrepareProvinces(IList<SelectListItem> items , int? countryId = null, string defaultItemText = null);

        /// <summary>
        /// prepare list cities
        /// </summary>
        /// <param name="items"></param>
        /// <param name="provinceId">provinceid</param>
        /// <param name="defaultItemText"></param>
        void PrepareCities(IList<SelectListItem> items,int? provinceId = null, string defaultItemText = null);
    }
}
