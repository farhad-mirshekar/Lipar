using Lipar.Core.Infrastructure;
using Lipar.Services.General.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Framework.Controllers
{
    public abstract class BaseController : Controller
    {
        public IActionResult AccessDeniedView()
        {
            return RedirectToAction("AccessDenid", "Security");
        }
        protected JsonResult AccessDeniedDataTablesJson()
        {
            var _localeStringResourceService = EngineContext.Current.Resolve<ILocaleStringResourceService>();
            return ErrorJson(_localeStringResourceService.GetResource("Admin.AccessDenied.Description"));
        }

        protected JsonResult ErrorJson(string error)
        {
            return Json(new
            {
                error = error
            });
        }
    }
}
