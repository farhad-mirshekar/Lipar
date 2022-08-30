using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Dashboard.Components
{
    [ViewComponent(Name ="Toolbar-Dashboard")]
    public class ToolbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
