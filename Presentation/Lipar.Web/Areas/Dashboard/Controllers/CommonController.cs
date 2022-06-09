using Lipar.Web.Areas.Dashboard.Factories;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Dashboard.Controllers
{
    public class CommonController : BaseDashboardController
    {
        #region Ctor
        public CommonController(ICommonModelFactory commonModelFactory)
        {
            _commonModelFactory = commonModelFactory;
        }
        #endregion

        #region Fields
        private readonly ICommonModelFactory _commonModelFactory;
        #endregion

        #region Methods
        public IActionResult PageNotFound()
            => View();

        public JsonResult Countries()
        {
            var counries = new List<SelectListItem>();
            _commonModelFactory.PrepareCountries(counries);

            return Json(counries);
        }

        public JsonResult Provinces(int? countryId = null)
        {
            var provinces = new List<SelectListItem>();
            _commonModelFactory.PrepareProvinces(provinces, countryId);

            return Json(provinces);
        }

        public JsonResult Cities(int? provinceId = null)
        {
            var cities = new List<SelectListItem>();
            _commonModelFactory.PrepareCities(cities, provinceId);

            return Json(cities);
        }
        #endregion
    }
}
