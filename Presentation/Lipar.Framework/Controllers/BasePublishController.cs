using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Framework.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class BasePublishController : BaseController
    {
        public IActionResult InvokeHttp404()
            => RedirectToAction("PageNotFound", "Common");
    }
}
