using Lipar.Web.Factories.Application;
using Lipar.Web.Models.Application;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Components
{
    public class ProductListViewComponent : ViewComponent
    {
        public ProductListViewComponent(IProductModelFactory productModelFactory)
        {
            _productModelFactory = productModelFactory;
        }

        private readonly IProductModelFactory _productModelFactory;
        public IViewComponentResult Invoke(ProductFilterModel filter)
        {
            var title = string.Empty;
            if (!string.IsNullOrEmpty(filter.Title))
            {
                title = filter.Title;
            }
            ViewBag.Title = title;

            if (filter.SpecialOffer.HasValue && filter.SpecialOffer.Value)
            {
                var model = _productModelFactory.SpecialOfferProducts();
                return View(model);
            }
            else
            {
                var model = _productModelFactory.LatestProducts();
                return View(model);
            }
        }
    }
}
