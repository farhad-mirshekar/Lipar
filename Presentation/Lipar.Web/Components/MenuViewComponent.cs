using Lipar.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Components
{
    /// <summary>
    ///show menu main
    /// </summary>
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICommonModelFactory _commonModelFactory;
        public MenuViewComponent(ICommonModelFactory commonModelFactory)
        {
            _commonModelFactory = commonModelFactory;
        }
        public IViewComponentResult Invoke()
        {
            var model = _commonModelFactory.PrepareMenuModel();
            return View(model);
        }
    }
}
