using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.Enums;
using Lipar.Services.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Infrastructure;
using Lipar.Web.Models;
using Lipar.Web.Models.Application;
using Lipar.Web.Models.General;
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
                                          , IMediaService mediaService)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _localeStringResourceService = localeStringResourceService;
            _productAttributeValueService = productAttributeValueService;
            _productMediaService = productMediaService;
            _mediaService = mediaService;
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

        public IList<ShoppingCartItemModel> PrepareShoppingCartItemListModel(Guid shoppingCartItemId)
        {
            var cacheKey = new CacheKey(LiparWebCacheKey.Shopping_Cart_List);

            return _cacheManager.Get(cacheKey, () =>
            {
                var query = _shoppingCartItemService.GetShoppingCartItemQuery(shoppingCartItemId);
                var result = query.Select(x => new ShoppingCartItemModel
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
                    ProductAttributeValues = CommonHelper.DeserializeObject<List<ProductAttributeValue>>(x.AttributeJson)
                }).ToList();

                foreach (var item in result)
                {
                    item.MediaModel = PrepareMediaModel(item.ProductId);
                }
                return result;
            });

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
        #endregion
    }
}
