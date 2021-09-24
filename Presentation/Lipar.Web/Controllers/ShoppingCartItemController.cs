using Lipar.Core;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Controllers
{
    public class ShoppingCartItemController : BaseController
    {
        #region Ctor
        public ShoppingCartItemController(IProductService productService
                                        , IWorkContext workContext
                                        , ILocaleStringResourceService localeStringResourceService)
        {
            _productService = productService;
            _workContext = workContext;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Fields
        private readonly IProductService _productService;
        private readonly IWorkContext _workContext;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Shopping Cart Item

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult ShoppingCartItemCreate(int productId, IFormCollection form)
        {
            if (_workContext.CurrentUser == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    NotyType = "error",
                    Message = _localeStringResourceService.GetResource("Web.ShoppingCartItem.YouMustBeLoggedIn")
                });
            }

            var product = _productService.GetById(productId, true);
            if (product == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    NotyType = "error",
                    Message = _localeStringResourceService.GetResource("Web.ShoppingCartItem.ProductNotFound")
                });
            }



            return Json(new ResultViewModel
            {

            });
        }
        #endregion
    }
}
