using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Dashboard.Controllers
{
    public class SecurityController : BaseDashboardController
    {
        public IActionResult AccessDenid()
        {
            return View();
        }
    }
}
