﻿using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.Enums;
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
                                          , IBankPortService bankPortService)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _localeStringResourceService = localeStringResourceService;
            _productAttributeValueService = productAttributeValueService;
            _productMediaService = productMediaService;
            _mediaService = mediaService;
            _userAddressService = userAddressService;
            _bankPortService = bankPortService;
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
        private readonly IBankPortService _bankPortService;
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

                            var productAttributeValueId = int.Parse(ctrlAttributes);
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

                            var shoppingCartItem = new ShoppingCartItem();
                            shoppingCartItem.ProductId = model.Id;
                            shoppingCartItem.Quantity = 1;
                            shoppingCartItem.UserId = _workContext.CurrentUser.Id;
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
                            shoppingCartItem.AttributeJson = CommonHelper.SerializeObject(productAttributeJson);

                            _shoppingCartItemService.Add(shoppingCartItem);

                            if (shoppingCartItem.Id > 0)
                            {
                                return new ResultViewModel
                                {
                                    Success = true,
                                    Message = _localeStringResourceService.GetResource("Web.ShoppingCartItem.AddToCart"),
                                    NotyType = "success",
                                };
                            }
                            break;
                    }
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

            var cart = _shoppingCartItemService.Get(_workContext.ShoppingCartItems.Value, product.Id);
            if (cart == null)
            {
                shoppingCartItem.Quantity = 1;
                _shoppingCartItemService.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity = cart.Quantity + 1;
                _shoppingCartItemService.Edit(shoppingCartItem);
            }

            return new ResultViewModel
            {
                Success = true,
                Message = _localeStringResourceService.GetResource("Web.ShoppingCartItem.AddToCart"),
                Url = "Cart"
            };
        }

        public ShoppingCartItemListModel PrepareShoppingCartItemListModel(Guid shoppingCartItemId)
        {
            var cacheKey = new CacheKey(LiparWebCacheKey.Shopping_Cart_List);

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
                    ProductAttributeValues = CommonHelper.DeserializeObject<List<ProductAttributeValue>>(x.AttributeJson)
                }).ToList();

                foreach (var shoppingCartItem in shoppingCartItemModels)
                {
                    shoppingCartItem.MediaModel = PrepareMediaModel(shoppingCartItem.ProductId);
                    shoppingCartItemListModel.CartAmount += CalculatePrice(shoppingCartItem.ProductDiscountTypeId, shoppingCartItem.ProductDiscount, shoppingCartItem.ProductPrice, shoppingCartItem.Quantity);
                }

                shoppingCartItemListModel.AvailableShoppingCartItemModels = shoppingCartItemModels;

                var shippingCost = shoppingCartItemModels.Where(x => x.ShippingCost != null).OrderByDescending(x => x.ShippingCost.Priority).Select(x => x.ShippingCost).FirstOrDefault();

                shoppingCartItemListModel.CartAmount = CalculateShippingCost(shoppingCartItemListModel.CartAmount, shippingCost);

                return shoppingCartItemListModel;
            });

        }

        public MiniShoppingCartItemModel PrepareMiniShoppingCartItemModel(Guid shoppingCartItemId)
        {
            var cacheKey = new CacheKey(LiparWebCacheKey.Mini_Shopping_Cart_List);

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
                    ProductAttributeValues = CommonHelper.DeserializeObject<List<ProductAttributeValue>>(x.AttributeJson)
                }).ToList();

                miniShoppingCartItemModel.ShoppingCartItemId = shoppingCartItemId;
                miniShoppingCartItemModel.AmountProducts = shoppingCartItemModels.Sum(x => x.Quantity * x.ProductPrice);

                foreach (var shoppingCartItem in shoppingCartItemModels)
                {
                    miniShoppingCartItemModel.CartDiscountAmount += CalculateDiscount(shoppingCartItem.ProductDiscountTypeId, shoppingCartItem.ProductDiscount, shoppingCartItem.ProductPrice);
                }

                miniShoppingCartItemModel.CartAmount = miniShoppingCartItemModel.AmountProducts - (miniShoppingCartItemModel.CartDiscountAmount ?? 0);
                miniShoppingCartItemModel.DeliveryDate = shoppingCartItemModels.OrderByDescending(cart => cart.DeliveryDate.Priority).Select(cart=>cart.DeliveryDate).FirstOrDefault();
                miniShoppingCartItemModel.ShippingCost = shoppingCartItemModels.Where(cart => cart.ShippingCost != null).OrderByDescending(cart => cart.ShippingCost.Priority).Select(cart => cart.ShippingCost).FirstOrDefault();

                if(miniShoppingCartItemModel.ShippingCost != null)
                {
                    miniShoppingCartItemModel.CartAmount = miniShoppingCartItemModel.CartAmount - miniShoppingCartItemModel.ShippingCost.Price;
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

                return miniShoppingCartItemModel;
            });
        }

        public IList<BankModel> PrepareBankList()
        {
            var bankPorts = _bankPortService.GetBankPorts();

            var result = bankPorts.Select(bankPort => new BankModel
            {
                Id = bankPort.Id,
                Name = bankPort.Bank.Name,
                CreationDate = bankPort.CreationDate,
                PaymentUri = bankPort.Bank.PaymentUri,
                RedirectUri = bankPort.Bank.RedirectUri,
                BankPort = new BankPortModel
                {
                    BankId = bankPort.BankId,
                    Id = bankPort.Id,
                    CreationDate = bankPort.CreationDate,
                    MerchantId = bankPort.MerchantId,
                    MerchantKey = bankPort.MerchantKey,
                    Name = bankPort.Name,
                    Password = bankPort.Password,
                    TerminalId = bankPort.TerminalId,
                    Username = bankPort.Username,
                }
            }).ToList();

            return result;
        }
        #endregion

        #region Utilities
        protected MediaModel PrepareMediaModel(int productId)
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
        private decimal CalculatePrice(int? ProductDiscountTypeId, decimal? discount, decimal price, int quantity)
        {
            switch (ProductDiscountTypeId)
            {
                case (int)DiscountTypeEnum.Amount:
                    price = price - (discount ?? 0);
                    break;

                case (int)DiscountTypeEnum.Percentage:
                    price = (price * (discount ?? 1)) / 100;
                    break;
            }

            if (quantity == 0)
            {
                quantity = 1;
            }

            price = price * quantity;
            return price;
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
                price = price - shippingCost.Price;
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
            switch (ProductDiscountTypeId)
            {
                case (int)DiscountTypeEnum.Amount:
                    price = price - (discount ?? 0);
                    break;
                case (int)DiscountTypeEnum.Percentage:
                    price = (price * (discount ?? 1)) / 100;
                    break;
            }

            return price;
        }
        #endregion
    }
}
