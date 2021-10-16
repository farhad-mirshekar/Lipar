using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Factories.Application;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Infrastructure;
using Lipar.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Lipar.Web.Controllers
{
    public class ShoppingCartItemController : BaseController
    {
        #region Ctor
        public ShoppingCartItemController(IProductService productService
                                        , IWorkContext workContext
                                        , ILocaleStringResourceService localeStringResourceService
                                        , IShoppingCartItemModelFactory shoppingCartItemModelFactory
                                        , IProductModelFactory productModelFactory
                                        , IShoppingCartItemService shoppingCartItemService
                                        , IStaticCacheManager cacheManager)
        {
            _productService = productService;
            _workContext = workContext;
            _localeStringResourceService = localeStringResourceService;
            _shoppingCartItemModelFactory = shoppingCartItemModelFactory;
            _productModelFactory = productModelFactory;
            _shoppingCartItemService = shoppingCartItemService;
            _cacheManager = cacheManager;
        }
        #endregion

        #region Fields
        private readonly IProductService _productService;
        private readonly IWorkContext _workContext;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IShoppingCartItemModelFactory _shoppingCartItemModelFactory;
        private readonly IProductModelFactory _productModelFactory;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IStaticCacheManager _cacheManager;
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

            if (product.ProductAttributeMappings != null && product.ProductAttributeMappings.Any())
            {
                var model = _productModelFactory.PrepareProductModel(new Models.Application.ProductModel(), product, false);
                var resultViewModel = _shoppingCartItemModelFactory.AddToCartWithAttribute(model, form);
                return Json(resultViewModel);
            }
            else
            {
                var resultViewModel = _shoppingCartItemModelFactory.AddToCartWithoutAttribute(product);
                return Json(resultViewModel);
            }
        }

        public IActionResult Cart()
        {
            if (_workContext.CurrentUser == null)
            {
                return RedirectToRoute("Homepage");
            }

            if (_workContext.ShoppingCartItems == null)
            {
                return RedirectToRoute("Homepage");
            }

            var model = _shoppingCartItemModelFactory.PrepareShoppingCartItemListModel(_workContext.ShoppingCartItems.Value);

            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult CartItemPlus(int id)
        {
            var shoppingCartItem = _shoppingCartItemService.GetById(id);
            if (shoppingCartItem == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    NotyType = "error",
                    Message = ""
                });
            }

            shoppingCartItem.Quantity += 1;
            _shoppingCartItemService.Edit(shoppingCartItem);

            //clear cache shopping cart list
            var cacheKey = new CacheKey(LiparWebCacheKey.Shopping_Cart_List);
            _cacheManager.Remove(cacheKey);

            var model = _shoppingCartItemModelFactory.PrepareShoppingCartItemListModel(_workContext.ShoppingCartItems.Value);
            return Json(new
            {
                Success = true,
                NotyType = "success",
                Message = "",
                Html = RenderPartialViewToString("_Cart", model),
                DivName="#cart"

            });
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult CartItemMinus(int id)
        {
            var shoppingCartItem = _shoppingCartItemService.GetById(id);
            if (shoppingCartItem == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    NotyType = "error",
                    Message = ""
                });
            }

            shoppingCartItem.Quantity -= 1;
            _shoppingCartItemService.Edit(shoppingCartItem);

            //clear cache shopping cart list
            var cacheKey = new CacheKey(LiparWebCacheKey.Shopping_Cart_List);
            _cacheManager.Remove(cacheKey);

            var model = _shoppingCartItemModelFactory.PrepareShoppingCartItemListModel(_workContext.ShoppingCartItems.Value);
            return Json(new
            {
                Success = true,
                NotyType = "success",
                Message = "",
                Html = RenderPartialViewToString("_Cart", model),
                DivName = "#cart"

            });
        }
        #endregion
    }
}
