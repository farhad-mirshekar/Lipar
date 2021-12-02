using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Dashboard.Controllers
{
    public class CommonController : BaseAdminController
    {
        public IActionResult PageNotFound()
            => View();
    }
}
