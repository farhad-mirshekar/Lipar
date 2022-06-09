using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Dashboard.Factories
{
    public interface ICommonModelFactory
    {
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
        void PrepareProvinces(IList<SelectListItem> items, int? countryId = null, string defaultItemText = null);

        /// <summary>
        /// prepare list cities
        /// </summary>
        /// <param name="items"></param>
        /// <param name="provinceId">provinceid</param>
        /// <param name="defaultItemText"></param>
        void PrepareCities(IList<SelectListItem> items, int? provinceId = null, string defaultItemText = null);
    }
}
