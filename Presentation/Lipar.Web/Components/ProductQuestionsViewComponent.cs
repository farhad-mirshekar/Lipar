using Lipar.Services.Application.Contracts;
using Lipar.Web.Factories.Application;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Components
{
    public class ProductQuestionsViewComponent : ViewComponent
    {
        public ProductQuestionsViewComponent(IProductService productService
                                           , IProductModelFactory productModelFactory)
        {
            _productService = productService;
            _productModelFactory = productModelFactory;
        }

        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;

        public IViewComponentResult Invoke(int productId)
        {
            var product = _productService.GetById(productId, true);

            var productQuestions = _productModelFactory.PrepareProductQuestionListModel(product);

            ViewBag.AllowCustomerReviews = product.AllowCustomerReviews;

            return View(productQuestions);
        }
    }
}
