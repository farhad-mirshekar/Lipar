using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.Enums;
using Lipar.Entities.Domain.Financial;
using Lipar.Entities.Domain.Financial.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.Financial.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Infrastructure;
using Lipar.Web.Models;
using Lipar.Web.Models.Application;
using Lipar.Web.Models.Financial;
using Lipar.Web.Models.General;
using Lipar.Web.Models.Organization;
using Melli.Portal.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Factories.Application
{
    public class ShoppingCartItemModelFactory : IShoppingCartItemModelFactory
    {
        #region Ctor
        public ShoppingCartItemModelFactory(IShoppingCartItemService shoppingCartItemService
                                          , IStaticCacheManager cacheManager
                                          , IWorkContext workContext
                                          , ILocaleStringResourceService localeStringResourceService
                                          , IProductAttributeValueService productAttributeValueService
                                          , IProductMediaService productMediaService
                                          , IMediaService mediaService
                                          , IUserAddressService userAddressService
                                          , IBankService bankService
                                          , IOrderService orderService
                                          , IBankPortService bankPortService
                                          , IOrderPaymentStatusService orderPaymentStatusService)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _localeStringResourceService = localeStringResourceService;
            _productAttributeValueService = productAttributeValueService;
            _productMediaService = productMediaService;
            _mediaService = mediaService;
            _userAddressService = userAddressService;
            _bankService = bankService;
            _orderService = orderService;
            _bankPortService = bankPortService;
            _orderPaymentStatusService = orderPaymentStatusService;
        }
        #endregion

        #region Fields
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IProductAttributeValueService _productAttributeValueService;
        private readonly IProductMediaService _productMediaService;
        private readonly IMediaService _mediaService;
        private readonly IUserAddressService _userAddressService;
        private readonly IBankService _bankService;
        private readonly IOrderService _orderService;
        private readonly IBankPortService _bankPortService;
        private readonly IOrderPaymentStatusService _orderPaymentStatusService;
        #endregion

        #region Methods
        public ResultViewModel AddToCartWithAttribute(ProductModel model, IFormCollection formCollection)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (_workContext.ShoppingCartItems == null)
            {
                throw new ArgumentNullException(nameof(_workContext.ShoppingCartItems));
            }

            if (_workContext.CurrentUser == null)
            {
                return new ResultViewModel
                {
                    Success = false,
                    NotyType = "error",
                    Message = _localeStringResourceService.GetResource("Web.ShoppingCartItem.YouMustBeLoggedIn")
                };
            }

            var productAttributes = new List<ProductAttributeValue>();

            if (model.ProductAttributes != null && model.ProductAttributes.Count > 0)
            {
                foreach (var productAttribute in model.ProductAttributes)
                {
                    string controlId = $"{LiparApplicationDefaults.ProductAttributePrefix}{productAttribute.ProductAttributeId}";
                    switch (productAttribute.AttributeControlTypeId)
                    {
                        case (int)AttributeControlTypeEnum.Dropdown:
                            var ctrlAttributes = formCollection[controlId];
                            if (ctrlAttributes == "-1")
                            {
                                return new ResultViewModel
                                {
                                    Success = false,
                                    NotyType = "error",
                                    Message = string.Format(_localeStringResourceService.GetResource("Web.ShoppingCartItem.YouMustSelectAttribute"), productAttribute.TextPrompt)
                                };
                            }

                            var productAttributeValueId = Guid.Parse(ctrlAttributes);
                            var productAttributeValueModel = productAttribute.ProductAttributeValues.FirstOrDefault(x => x.Id == productAttributeValueId);
                            if (productAttributeValueModel == null)
                            {
                                throw new ArgumentNullException(nameof(productAttributeValueModel));
                            }

                            var productAttributeValue = _productAttributeValueService.GetById(productAttributeValueModel.Id, true);
                            if (productAttributeValue == null)
                            {
                                throw new ArgumentNullException(nameof(productAttributeValue));
                            }

                            var productAttributeJson = new ProductAttributeValue
                            {
                                Id = productAttributeValue.Id,
                                Name = productAttributeValue.Name,
                                Price = productAttributeValue.Price,
                                ProductAttributeMappingId = productAttributeValue.ProductAttributeMappingId,
                                ProductAttributeMapping = new ProductAttributeMapping
                                {
                                    Id = productAttributeValue.ProductAttributeMapping.Id,
                                    AttributeControlTypeId = productAttributeValue.ProductAttributeMapping.AttributeControlTypeId,
                                    ProductAttributeId = productAttributeValue.ProductAttributeMapping.ProductAttributeId,
                                    ProductId = model.Id,
                                    TextPrompt = productAttributeValue.ProductAttributeMapping.TextPrompt
                                }
                            };

                            productAttributes.Add(productAttributeJson);

                            break;
                    }
                }

                var shoppingCartItem = new ShoppingCartItem();
                shoppingCartItem.ProductId = model.Id;
                shoppingCartItem.UserId = _workContext.CurrentUser.Id;
                shoppingCartItem.CreationDate = CommonHelper.GetCurrentDateTime();
                if (productAttributes != null && productAttributes.Any())
                {
                    shoppingCartItem.AttributeJson = CommonHelper.SerializeObject(productAttributes);
                }
                shoppingCartItem.ShoppingCartItemId = _workContext.ShoppingCartItems.Value;

                var cart = _shoppingCartItemService.Get(_workContext.ShoppingCartItems.Value, model.Id);
                if (cart == null)
                {
                    shoppingCartItem.Quantity = 1;
                    _shoppingCartItemService.Add(shoppingCartItem);
                }
                else
                {
                    cart.Quantity += 1;
                    _shoppingCartItemService.Edit(cart);
                    shoppingCartItem.Id = cart.Id;
                }

                if (shoppingCartItem.Id != Guid.Empty)
                {
                    //clear cache shopping cart
                    ClearCacheShoppingCart();

                    return new ResultViewModel
                    {
                        Success = true,
                        Message = _localeStringResourceService.GetResource("Web.ShoppingCartItem.AddToCart"),
                        NotyType = "success",
                        Url ="/Cart"
                    };
                }
            }

            return new ResultViewModel
            {
                Success = false,
                NotyType = "error",
                Message = "خطای ناشناخته"
            };

        }

        public ResultViewModel AddToCartWithoutAttribute(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (_workContext.ShoppingCartItems == null)
            {
                throw new ArgumentNullException("ShoppingCartItems");
            }

            if (_workContext.CurrentUser == null)
            {
                return new ResultViewModel
                {
                    Success = false,
                    NotyType = "error",
                    Message = _localeStringResourceService.GetResource("Web.ShoppingCartItem.YouMustBeLoggedIn")
                };
            }

            var shoppingCartItem = new ShoppingCartItem();
            shoppingCartItem.ProductId = product.Id;
            shoppingCartItem.ShoppingCartItemId = _workContext.ShoppingCartItems.Value;
            shoppingCartItem.UserId = _workContext.CurrentUser.Id;
            shoppingCartItem.CreationDate = CommonHelper.GetCurrentDateTime();

            var cart = _shoppingCartItemService.Get(_workContext.ShoppingCartItems.Value, product.Id);
            if (cart == null)
            {
                shoppingCartItem.Quantity = 1;
                _shoppingCartItemService.Add(shoppingCartItem);
            }
            else
            {
                cart.Quantity += 1;
                _shoppingCartItemService.Edit(cart);
            }

            //clear cache shopping cart
            ClearCacheShoppingCart();

            return new ResultViewModel
            {
                Success = true,
                Message = _localeStringResourceService.GetResource("Web.ShoppingCartItem.AddToCart"),
                Url = "/Cart",
                NotyType = "success",
            };
        }

        public ShoppingCartItemListModel PrepareShoppingCartItemListModel(Guid shoppingCartItemId)
        {
            var cacheKey = new CacheKey(LiparWebCacheKey.LiparShoppingCartList);

            return _cacheManager.Get(cacheKey, () =>
            {
                var shoppingCartItemListModel = new ShoppingCartItemListModel();

                var query = _shoppingCartItemService.GetShoppingCartItemQuery(shoppingCartItemId);
                var shoppingCartItemModels = query.Select(x => new ShoppingCartItemModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    UserId = x.UserId,
                    Quantity = x.Quantity,
                    CreationDate = x.CreationDate,
                    ShoppingCartItemId = x.ShoppingCartItemId,
                    CategoryId = x.Product.CategoryId,
                    CategoryName = x.Product.Category.Name,
                    ProductPrice = x.Product.Price,
                    ProductDiscount = x.Product.Discount,
                    ProductDiscountTypeId = x.Product.DiscountTypeId,
                    DeliveryDate = x.Product.DeliveryDate != null ? new DeliveryDateModel
                    {
                        Id = x.Product.DeliveryDateId.Value,
                        Name = x.Product.DeliveryDate.Name,
                        Priority = x.Product.DeliveryDate.Priority,
                        Description = x.Product.DeliveryDate.Description,
                    }
                    : null,
                    ShippingCost = x.Product.ShippingCostId.HasValue ? new ShippingCostModel
                    {
                        Id = x.Product.ShippingCostId.Value,
                        Price = x.Product.ShippingCost.Price,
                        Name = x.Product.ShippingCost.Name,
                        Priority = x.Product.ShippingCost.Priority
                    }
                    : null,
                    ProductAttributeValues = !string.IsNullOrEmpty(x.AttributeJson)
                                             ? CommonHelper.DeserializeObject<List<ProductAttributeValue>>(x.AttributeJson)
                                             : null
                }).ToList();

                foreach (var shoppingCartItem in shoppingCartItemModels)
                {
                    shoppingCartItem.MediaModel = PrepareMediaModel(shoppingCartItem.ProductId);
                    shoppingCartItemListModel.CartAmount += CalculatePrice(shoppingCartItem);
                }

                if(shoppingCartItemModels != null && shoppingCartItemModels.Any())
                {
                    shoppingCartItemListModel.AvailableShoppingCartItemModels = shoppingCartItemModels;

                    var shippingCost = shoppingCartItemModels.Where(x => x.ShippingCost != null).OrderByDescending(x => x.ShippingCost.Priority).Select(x => x.ShippingCost).FirstOrDefault();

                    shoppingCartItemListModel.CartAmount = CalculateShippingCost(shoppingCartItemListModel.CartAmount, shippingCost);
                }

                return shoppingCartItemListModel;
            });

        }

        public MiniShoppingCartItemModel PrepareMiniShoppingCartItemModel(Guid shoppingCartItemId)
        {
            var cacheKey = new CacheKey(LiparWebCacheKey.LiparMiniShoppingCartList);

            return _cacheManager.Get(cacheKey, () =>
            {
                var miniShoppingCartItemModel = new MiniShoppingCartItemModel();

                var query = _shoppingCartItemService.GetShoppingCartItemQuery(shoppingCartItemId);
                var shoppingCartItemModels = query.Select(x => new ShoppingCartItemModel
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    ShoppingCartItemId = x.ShoppingCartItemId,
                    ProductPrice = x.Product.Price,
                    ProductDiscount = x.Product.Discount,
                    ProductDiscountTypeId = x.Product.DiscountTypeId,
                    DeliveryDate = x.Product.DeliveryDate != null ? new DeliveryDateModel
                    {
                        Id = x.Product.DeliveryDateId.Value,
                        Name = x.Product.DeliveryDate.Name,
                        Priority = x.Product.DeliveryDate.Priority,
                        Description = x.Product.DeliveryDate.Description,
                    }
                    : null,
                    ShippingCost = x.Product.ShippingCostId.HasValue ? new ShippingCostModel
                    {
                        Id = x.Product.ShippingCostId.Value,
                        Price = x.Product.ShippingCost.Price,
                        Name = x.Product.ShippingCost.Name,
                        Priority = x.Product.ShippingCost.Priority
                    }
                    : null,
                    ProductAttributeValues = !string.IsNullOrEmpty(x.AttributeJson)
                                             ? CommonHelper.DeserializeObject<List<ProductAttributeValue>>(x.AttributeJson)
                                             : null
                }).ToList();

                if(shoppingCartItemModels != null && shoppingCartItemModels.Any())
                {
                    miniShoppingCartItemModel.ShoppingCartItemId = shoppingCartItemId;
                    miniShoppingCartItemModel.AmountProducts = shoppingCartItemModels.Sum(x => x.Quantity * x.ProductPrice);

                    foreach (var shoppingCartItem in shoppingCartItemModels)
                    {
                        var discount = CalculateDiscount(shoppingCartItem.ProductDiscountTypeId, shoppingCartItem.ProductDiscount, shoppingCartItem.ProductPrice);
                        miniShoppingCartItemModel.CartDiscountAmount = miniShoppingCartItemModel.CartDiscountAmount.GetValueOrDefault(0) + discount;
                    }

                    miniShoppingCartItemModel.CartAmount = miniShoppingCartItemModel.AmountProducts - (miniShoppingCartItemModel.CartDiscountAmount ?? 0);
                    miniShoppingCartItemModel.DeliveryDate = shoppingCartItemModels.OrderByDescending(cart => cart.DeliveryDate?.Priority).Select(cart => cart.DeliveryDate).FirstOrDefault();
                    miniShoppingCartItemModel.ShippingCost = shoppingCartItemModels.Where(cart => cart.ShippingCost != null).OrderByDescending(cart => cart.ShippingCost.Priority).Select(cart => cart.ShippingCost).FirstOrDefault();

                    if (miniShoppingCartItemModel.ShippingCost != null)
                    {
                        miniShoppingCartItemModel.CartAmount += miniShoppingCartItemModel.ShippingCost.Price;
                    }
                    var userAddressList = _userAddressService.List(new UserAddressListVM
                    {
                        UserId = _workContext.CurrentUser.Id
                    });

                    foreach (var userAddress in userAddressList)
                    {
                        miniShoppingCartItemModel.AvailableUserAddress.Add(new UserAddressModel
                        {
                            Address = userAddress.Address,
                            Id = userAddress.Id,
                            CreationDate = userAddress.CreationDate,
                            PostalCode = userAddress.PostalCode,
                            UserId = userAddress.UserId,
                        });
                    }
                }

                return miniShoppingCartItemModel;
            });
        }

        public IList<BankModel> PrepareBankList()
        {
            var banks = _bankService.BankQuery(new Entities.Domain.Financial.BankListVM { });

            var result = banks.Select(bank => new BankModel
            {
                Id = bank.Id,
                Name = bank.Name,
                CreationDate = bank.CreationDate,
                PaymentUri = bank.PaymentUri,
                RedirectUri = bank.RedirectUri,
            }).ToList();

            return result;
        }

        public string Payment(Guid bankId, Guid addressId, Guid shoppingCartItemId)
        {
            var bank = _bankService.GetById(bankId, true);
            if (bank is null)
            {
                throw new ArgumentNullException(nameof(bank));
            }

            var miniShoppingCartItem = PrepareMiniShoppingCartItemModel(shoppingCartItemId);

            if (miniShoppingCartItem is null)
            {
                throw new ArgumentNullException(nameof(miniShoppingCartItem));
            }

            var token = string.Empty;
            var result = false;
            //
            switch (bank.BankTypeId)
            {
                case (int)BankTypeEnum.melli:
                    result = ConnectToBankMelli(bank, addressId, miniShoppingCartItem.CartAmount, shoppingCartItemId, out token);
                    break;
            }

            if (!string.IsNullOrEmpty(token))
            {
                var url = $"{bank.ServiceUri}?token={token}";

                return url;
            }

            return string.Empty;
        }

        public void DeleteAllShoppingCartItem(Guid shoppingCartItemsId)
        {
            if (shoppingCartItemsId == Guid.Empty)
            {
                throw new Exception("shopping cart item is null");
            }

            _shoppingCartItemService.Delete(shoppingCartItemsId);
        }

        public void ClearCacheShoppingCart()
        {
            _cacheManager.Remove(new CacheKey(LiparWebCacheKey.LiparShoppingCartList));
            _cacheManager.Remove(new CacheKey(LiparWebCacheKey.LiparMiniShoppingCartList));
        }
        #endregion

        #region Utilities
        protected MediaModel PrepareMediaModel(Guid productId)
        {
            var mediaModel = new MediaModel();
            var productMedia = _productMediaService.List(new ProductMediaListVM
            {
                ProductId = productId
            }).FirstOrDefault();

            if (productMedia != null)
            {
                var media = _mediaService.GetById(productMedia.MediaId);

                mediaModel.Id = productMedia.Id;
                mediaModel.Priority = productMedia.Priority;
                mediaModel.Name = media.Name;
                mediaModel.AltAttribute = media.AltAttribute;
                mediaModel.MimeType = media.MimeType;
                mediaModel.CreationDate = media.CreationDate;
                mediaModel.MediaId = productMedia.MediaId;
            }

            return mediaModel;

        }

        /// <summary>
        /// calculate price
        /// </summary>
        /// <param name="ProductDiscountTypeId"></param>
        /// <param name="discount"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        private decimal CalculatePrice(ShoppingCartItemModel shoppingCartItem)
        {
            if(shoppingCartItem is null)
            {
                throw new Exception("shopping cart item is null");
            }

            switch (shoppingCartItem.ProductDiscountTypeId)
            {
                case (int)DiscountTypeEnum.Amount:
                    shoppingCartItem.ProductPrice = shoppingCartItem.ProductPrice - (shoppingCartItem.ProductDiscount ?? 0);
                    break;

                case (int)DiscountTypeEnum.Percentage:
                    shoppingCartItem.ProductPrice = (shoppingCartItem.ProductPrice * (shoppingCartItem.ProductDiscount ?? 1)) / 100;
                    break;
            }

            if (shoppingCartItem.Quantity == 0)
            {
                shoppingCartItem.Quantity = 1;
            }

            shoppingCartItem.ProductPrice = shoppingCartItem.ProductPrice * shoppingCartItem.Quantity;

            if(shoppingCartItem.ProductAttributeValues != null && shoppingCartItem.ProductAttributeValues.Any())
            {
                shoppingCartItem.ProductAttributeValues.ForEach(attribute =>
                {
                    if(attribute.Price.HasValue && attribute.Price.Value != 0)
                    {
                        shoppingCartItem.ProductPrice += attribute.Price ?? 0;
                    }
                });
            }
            return shoppingCartItem.ProductPrice;
        }

        /// <summary>
        /// calculate shipping cost
        /// </summary>
        /// <param name="price"></param>
        /// <param name="shippingCost"></param>
        /// <returns></returns>
        private decimal CalculateShippingCost(decimal price, ShippingCostModel shippingCost)
        {
            if (shippingCost != null)
            {
                price = price + shippingCost.Price;
            }
            else
            {
                var orderSettings = _orderService.OrderSettings();
                if (price < orderSettings.ShoppingCartRate)
                {
                    price = price + (orderSettings.ShoppingCartRate ?? 0);
                }
            }

            return price;
        }

        /// <summary>
        /// calculate discount
        /// </summary>
        /// <param name="ProductDiscountTypeId"></param>
        /// <param name="discount"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        private decimal CalculateDiscount(int? ProductDiscountTypeId, decimal? discount, decimal price)
        {
            decimal discountPrice = 0;
            switch (ProductDiscountTypeId)
            {
                case (int)DiscountTypeEnum.Amount:
                    discountPrice = discount.Value;
                    break;
                case (int)DiscountTypeEnum.Percentage:
                    discountPrice = (price * (discount ?? 1)) / 100;
                    break;
            }

            return discountPrice;
        }

        private bool ConnectToBankMelli(Bank bank, Guid userAddressId, decimal price, Guid shoppingCartItemId, out string token)
        {
            token = string.Empty;
            var result = false;

            try
            {
                if (bank is null)
                {
                    throw new ArgumentNullException(nameof(bank));
                }

                if (price == 0)
                {
                    throw new Exception("Price is not valid!");
                }

                var bankPort = _bankPortService.GetActiveBankPort(bank.Id, true);

                if (bankPort is null)
                {
                    throw new ArgumentNullException(nameof(bankPort));
                }

                var orderPaymentStatus = new OrderPaymentStatus();
                orderPaymentStatus.ShoppingCartItemId = shoppingCartItemId;
                orderPaymentStatus.OrderPaymentStatusTypeId = (int)OrderPaymentStatusTypeEnum.Unpaid;
                orderPaymentStatus.BankPortId = bankPort.Id;
                orderPaymentStatus.UserId = _workContext.CurrentUser.Id;
                orderPaymentStatus.UserAddressId = userAddressId;

                _orderPaymentStatusService.Add(orderPaymentStatus);

                var bankMelliService = new BankMelliService(new Melli.Portal.BankPortDetail
                {
                    MerchantId = bankPort.MerchantId,
                    MerchantKey = bankPort.MerchantKey,
                    PaymentUrl = bank.PaymentUri,
                    RedirectUrl = bank.RedirectUri,
                    TerminalId = bankPort.TerminalId,
                });

                var payResult = bankMelliService.PaymentRequest((long)price);

                if (payResult is null)
                {
                    throw new Exception("خطا در ارتباط با درگاه بانک");
                }

                if (!string.IsNullOrEmpty(payResult.Token))
                {
                    token = payResult.Token;
                }

                switch (payResult.ResCode)
                {
                    case (int)Melli.Portal.Model.ResCodeVerify.نتیجه_تراکنش_موفق_است:
                        orderPaymentStatus.Token = payResult.Token;
                        orderPaymentStatus.SignData = payResult.Request;
                        orderPaymentStatus.ResponseMessage = payResult.Description;
                        orderPaymentStatus.ResponseStatus = payResult.ResCode;

                        _orderPaymentStatusService.Edit(orderPaymentStatus);
                        result = true;

                        break;

                    default:

                        orderPaymentStatus.ResponseMessage = payResult.Description;
                        orderPaymentStatus.ResponseStatus = payResult.ResCode;
                        _orderPaymentStatusService.Edit(orderPaymentStatus);

                        result = false;
                        break;
                }

                return result;
            }
            catch (Exception e)
            {

            }

            return result;
        }
        #endregion
    }
}
