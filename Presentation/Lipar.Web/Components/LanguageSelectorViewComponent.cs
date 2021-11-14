using Lipar.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Components
{
    /// <summary>
    /// language selector
    /// </summary>
    public class LanguageSelectorViewComponent : ViewComponent
    {
        #region Ctor
        public LanguageSelectorViewComponent(ICommonModelFactory commonModelFactory)
        {
            _commonModelFactory = commonModelFactory;
        }
        #endregion

        #region Fields
        private readonly ICommonModelFactory _commonModelFactory;
        #endregion

        public IViewComponentResult Invoke()
        {
            var model = _commonModelFactory.PrepareLanguageSelectorModel();

            return View(model);
        }
    }
}
