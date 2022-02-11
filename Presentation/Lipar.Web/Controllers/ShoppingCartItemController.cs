using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Factories.Application;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Infrastructure;
using Lipar.Web.Models;
using Lipar.Web.Models.Organization;
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
                                        , IStaticCacheManager cacheManager
                                        , IUserAddressService userAddressService)
        {
            _productService = productService;
            _workContext = workContext;
            _localeStringResourceService = localeStringResourceService;
            _shoppingCartItemModelFactory = shoppingCartItemModelFactory;
            _productModelFactory = productModelFactory;
            _shoppingCartItemService = shoppingCartItemService;
            _cacheManager = cacheManager;
            _userAddressService = userAddressService;
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
        private readonly IUserAddressService _userAddressService;
        #endregion

        #region Shopping Cart Item

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult ShoppingCartItemCreate(Guid productId, IFormCollection form)
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
        public IActionResult CartItemPlus(Guid id)
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
        public IActionResult CartItemMinus(Guid id)
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

        #region Checkout Method
        public IActionResult Checkout()
        {
            if (_workContext.CurrentUser == null)
            {
                return RedirectToRoute("Homepage");
            }

            if (_workContext.ShoppingCartItems == null)
            {
                return RedirectToRoute("Homepage");
            }

            var shoppingCartItem = _workContext.ShoppingCartItems.Value;
            var model = _shoppingCartItemModelFactory.PrepareMiniShoppingCartItemModel(shoppingCartItem);

            return View(model);
        }
        #endregion

        #region User Address Method
        public IActionResult UserAddressCreate()
        {
            if(_workContext.CurrentUser == null)
            {
                return null;
            }
            var userAddressModel = new UserAddressModel();
            userAddressModel.UserId = _workContext.CurrentUser.Id;

            return View(userAddressModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserAddressCreate(UserAddressModel model)
        {
            if (_workContext.CurrentUser == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    Message = _localeStringResourceService.GetResource("Web.UserAddress.YouMustBeLoggedIn"),
                    NotyType = "error"
                });
            }

            if (ModelState.IsValid)
            {

                _userAddressService.Add(new UserAddress
                {
                    UserId = model.UserId,
                    Address = model.Address,
                    PostalCode = model.PostalCode
                });

                return Json(new 
                {
                    Success = true,
                    NotyType = "success",
                    Message = "آدرس با موفقیت ایجاد شد",
                    Html = RenderViewComponentToString("UserAddress"),
                    DivName = "#user-address-box"
                });
            }
            return Json(new ResultViewModel
            {
                Success = false,
                Message = _localeStringResourceService.GetResource("Web.UserAddress.YouMustBeLoggedIn"),
                NotyType = "error",
            });
        }

        public IActionResult UserAddressEdit(Guid id)
        {
            if(id == Guid.Empty)
            {
                return null;
            }

            var userAddress = _userAddressService.GetById(id);
            if(userAddress == null)
            {
                return null;
            }

            var userAddressModel = new UserAddressModel
            {
                Id = userAddress.Id,
                UserId = userAddress.UserId,
                PostalCode = userAddress.PostalCode,
                Address = userAddress.Address,
            };

            return View(userAddressModel);
        }

        [HttpPost]
        public IActionResult UserAddressEdit(UserAddressModel model)
        {
            if (_workContext.CurrentUser == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    Message = _localeStringResourceService.GetResource("Web.UserAddress.YouMustBeLoggedIn"),
                    NotyType = "error"
                });
            }

            if (ModelState.IsValid)
            {
                var userAddress = _userAddressService.GetById(model.Id);
                userAddress.Address = model.Address;
                userAddress.PostalCode = model.PostalCode;

                _userAddressService.Edit(userAddress);

                return Json(new
                {
                    Success = true,
                    NotyType = "success",
                    Message = "",
                    Html = RenderViewComponentToString("UserAddress"),
                    DivName = "#user-address-box"
                });
            }
            return Json(new ResultViewModel
            {
                Success = false,
                Message = _localeStringResourceService.GetResource("Web.UserAddress.YouMustBeLoggedIn"),
                NotyType = "error",
            });
        }

        [HttpPost]
        public IActionResult UserAddressDelete(Guid id)
        {
            if (_workContext.CurrentUser == null)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    Message = _localeStringResourceService.GetResource("Web.UserAddress.YouMustBeLoggedIn"),
                    NotyType = "error"
                });
            }

            var userAddressList = _userAddressService.List(new UserAddressListVM
            {
                UserId = _workContext.CurrentUser.Id
            });

            if(!userAddressList.Any(u=>u.Id == id))
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    Message ="شما قادر به حذف این آدرس نمی باشید",
                    NotyType = "error"
                });
            }

            var userAddress = _userAddressService.GetById(id);
            _userAddressService.Delete(userAddress);

            return Json(new
            {
                Success = true,
                NotyType = "success",
                Message = "",
                Html = RenderViewComponentToString("UserAddress"),
                DivName = "#user-address-box"
            });
        }
        #endregion
    }
}
