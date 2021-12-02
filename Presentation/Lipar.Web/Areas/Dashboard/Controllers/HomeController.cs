using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Dashboard.Controllers
{
    public class HomeController : BaseDashboardController
    {
        public IActionResult Index()
            => View();
    }
}
