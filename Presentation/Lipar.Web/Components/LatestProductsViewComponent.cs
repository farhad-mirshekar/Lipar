using Lipar.Web.Factories.Application;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Components
{
    public class LatestProductsViewComponent : ViewComponent
    {
        public LatestProductsViewComponent(IProductModelFactory productModelFactory)
        {
            _productModelFactory = productModelFactory;
        }

        private readonly IProductModelFactory _productModelFactory;
        public IViewComponentResult Invoke()
        {
            var model = _productModelFactory.LatestProducts();

            return View(model);
        }
    }
}
