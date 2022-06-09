using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Factories.Application;
using Lipar.Web.Factories.Organization;
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
                                        , IUserAddressService userAddressService
                                        , IUserModelFactory userModelFactory)
        {
            _productService = productService;
            _workContext = workContext;
            _localeStringResourceService = localeStringResourceService;
            _shoppingCartItemModelFactory = shoppingCartItemModelFactory;
            _productModelFactory = productModelFactory;
            _shoppingCartItemService = shoppingCartItemService;
            _cacheManager = cacheManager;
            _userAddressService = userAddressService;
            _userModelFactory = userModelFactory;
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
        private readonly IUserModelFactory _userModelFactory;
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
            _shoppingCartItemModelFactory.ClearCacheShoppingCart();

            var model = _shoppingCartItemModelFactory.PrepareShoppingCartItemListModel(_workContext.ShoppingCartItems.Value);
            return Json(new
            {
                Success = true,
                NotyType = "success",
                Message = "تغییرات با موفقیت انجام شد",
                Html = RenderPartialViewToString("_Cart", model),
                DivName = "#cart"

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
            if (shoppingCartItem.Quantity <= 0)
            {
                shoppingCartItem.Quantity = 1;
            }
            _shoppingCartItemService.Edit(shoppingCartItem);

            //clear cache shopping cart list
            _shoppingCartItemModelFactory.ClearCacheShoppingCart();

            var model = _shoppingCartItemModelFactory.PrepareShoppingCartItemListModel(_workContext.ShoppingCartItems.Value);
            return Json(new
            {
                Success = true,
                NotyType = "success",
                Message = "تغییرات با موفقیت انجام شد",
                Html = RenderPartialViewToString("_Cart", model),
                DivName = "#cart"

            });
        }

        public JsonResult GetShoppingCart()
        {
            return Json(new
            {
                Html = RenderViewComponentToString("MiniShoppingCart")
            });
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    NotyType = "error",
                    Message = "درخواست مورد نظر قابل اجرا نمی باشد"
                });
            }

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

            //delete shopping cart item
            _shoppingCartItemService.Delete(shoppingCartItem);

            //clear cache shopping cart list
            _shoppingCartItemModelFactory.ClearCacheShoppingCart();

            var model = _shoppingCartItemModelFactory.PrepareShoppingCartItemListModel(_workContext.ShoppingCartItems.Value);
            return Json(new
            {
                Success = true,
                NotyType = "success",
                Message = "حذف با موفقیت انجام شد",
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

        [HttpPost]
        public IActionResult Payment(Guid bankId, Guid addressId)
        {
            if (bankId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bankId));
            }

            if (addressId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(addressId));
            }

            var shoppingCartItemId = _workContext.ShoppingCartItems;

            if (!shoppingCartItemId.HasValue || (shoppingCartItemId.HasValue
                                               && shoppingCartItemId.Value == Guid.Empty))
            {
                throw new ArgumentNullException(nameof(shoppingCartItemId));
            }

            var url = _shoppingCartItemModelFactory.Payment(bankId, addressId, shoppingCartItemId.Value);

            if (!string.IsNullOrEmpty(url))
            {
                return Json(new ResultViewModel
                {
                    Success = true,
                    NotyType = "success",
                    Message = "درحال اتصال به درگاه بانک",
                    Url = url
                });
            }

            return Json(new ResultViewModel
            {
                Success = false,
                NotyType = "error",
                Message = "خطا در ارتباط با درگاه بانک"
            });
        }
        #endregion

        #region User Address Method
        public IActionResult UserAddressCreate()
        {
            if (_workContext.CurrentUser == null)
            {
                return null;
            }

            var model = _userModelFactory.PrepareUserAddressModel(new UserAddressModel(), null);
            return View(model);
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
                var userAddress = new UserAddress
                {
                    Address = model.Address,
                    PostalCode = model.PostalCode,
                    CountryId = model.CountryId,
                    ProvinceId = model.ProvinceId,
                    CityId = model.CityId,
                };
                _userAddressService.Add(userAddress);

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
            if (id == Guid.Empty)
            {
                return null;
            }

            var userAddress = _userAddressService.GetById(id);
            if (userAddress == null)
            {
                return null;
            }

            var model = _userModelFactory.PrepareUserAddressModel(new UserAddressModel(), userAddress);

            return View(model);
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
                userAddress.CountryId = model.CountryId;
                userAddress.ProvinceId = model.ProvinceId;
                userAddress.CityId = model.CityId;

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

            if (!userAddressList.Any(u => u.Id == id))
            {
                return Json(new ResultViewModel
                {
                    Success = false,
                    Message = "شما قادر به حذف این آدرس نمی باشید",
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
