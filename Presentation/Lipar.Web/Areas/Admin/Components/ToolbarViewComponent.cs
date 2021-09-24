using Lipar.Core;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Components
{
    public class ToolbarViewComponent : ViewComponent
    {
        private readonly IWorkContext _workContext;
        public ToolbarViewComponent(IWorkContext workContext)
        {
            _workContext = workContext;
        }
        public IViewComponentResult Invoke()
        {
            var model = _workContext.Positions;
            return View(model);
        }
    }
}
