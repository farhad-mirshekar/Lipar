using Lipar.Web.Framework.MVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Framework.Controllers
{
    [Area(AreaNames.Dashboard)]
    [AuthorizeDashboard]
    public class BaseDashboardController : BaseController
    {
        public IActionResult InvokeHttp404()
            => RedirectToAction("PageNotFound", "Common");
    }
}
