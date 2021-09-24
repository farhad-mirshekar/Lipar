using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class SecurityController : BaseAdminController
    {
        public IActionResult AccessDenid()
        {
            return View();
        }
    }
}
