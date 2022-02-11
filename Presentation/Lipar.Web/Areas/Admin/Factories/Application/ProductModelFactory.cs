using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class ProductModelFactory : IProductModelFactory
    {
        #region Ctor
        public ProductModelFactory(IProductService productService
                                 , IBaseAdminModelFactory baseAdminModelFactory
                                 , IProductAttributeMappingService productAttributeMappingService
                                 , IProductAttributeValueService productAttributeValueService
                                 , IProductMediaService productMediaService
                                 , IMediaService mediaService
                                 , IRelatedProductService relatedProductService)
        {
            _productService = productService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _productAttributeMappingService = productAttributeMappingService;
            _productAttributeValueService = productAttributeValueService;
            _productMediaService = productMediaService;
            _mediaService = mediaService;
            _relatedProductService = relatedProductService;
        }
        #endregion

        #region Fields
        private readonly IProductService _productService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IProductAttributeMappingService _productAttributeMappingService;
        private readonly IProductAttributeValueService _productAttributeValueService;
        private readonly IProductMediaService _productMediaService;
        private readonly IMediaService _mediaService;
        private readonly IRelatedProductService _relatedProductService;
        #endregion

        #region Methods
        public ProductListModel PrepareProductListModel(ProductSearchModel searchModel)
        {
            var products = _productService.List(new ProductListVM
            {
                Name = searchModel.Name,
                PriceFrom = searchModel.PriceFrom,
                PriceTo = searchModel.PriceTo,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            var models = new ProductListModel().PrepareToGrid(searchModel, products, () =>
            {
                return products.Select(product =>
                {
                    var productModel = product.ToModel<ProductModel, Guid>();

                    return productModel;
                });
            });

            return models;
        }
        public ProductModel PrepareProductModel(ProductModel model, Product product)
        {
            if (product != null)
            {
                model = product.ToModel<ProductModel, Guid>();

                //prepare product attribute mapping
                PrepareProductAttributeMappingSearchModel(model.ProductAttributeMappingSearchModel, product);

                //prepare product media 
                PrepareProductMediaSearchModel(model.ProductMediaSearchModel, product);

                //prepare related product
                PrepareRelatedProductSearchModel(model.RelatedProductSearchModel, product);
            }

            //gets all categories
            model.AvailableCategories = _baseAdminModelFactory.PrepareCategoriesForProduct();

            //gets all delivery date
            model.AvailableDeliveryDate = _baseAdminModelFactory.PrepareDeliveryDate();

            //gets all shipping cost
            model.AvailableShippingCost = _baseAdminModelFactory.PrepareShippingCost();

            //gets discount type
            _baseAdminModelFactory.PrepareDiscountType(model.AvailableDiscountType);

            return model;
        }
        public ProductAttributeMappingListModel PrepareProductAttributeMappingListModel(ProductAttributeMappingSearchModel searchModel, Product product)
        {
            var productAttributeMappings = _productAttributeMappingService.List(new ProductAttributeMappingListVM
            {
                ProductId = searchModel.ProductId,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.Page,
            });

            if (productAttributeMappings == null)
            {
                return null;
            }

            var model = new ProductAttributeMappingListModel().PrepareToGrid(searchModel, productAttributeMappings, () =>
            {
                return productAttributeMappings.Select(productAttributeMapping =>
                {
                    var productAttributeMappingModel = productAttributeMapping.ToModel<ProductAttributeMappingModel, Guid>();

                    return productAttributeMappingModel;
                });
            });

            return model;
        }
        public ProductAttributeMappingModel PrepareProductAttributeMappingModel(ProductAttributeMappingModel model, Product product, ProductAttributeMapping productAttributeMapping)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (productAttributeMapping != null)
            {
                model = productAttributeMapping.ToModel<ProductAttributeMappingModel, Guid>();

                //prepare product attribute value
                PrepareProductAttributeValueSearchModel(model.ProductAttributeValueSearchModel, productAttributeMapping);
            }

            model.ProductId = product.Id;

            //prepare attribute control type
            _baseAdminModelFactory.PrepareAttributeControlType(model.AvailableAttributeControlType);

            //prepare all attributes
            model.AvailableAttributes = _baseAdminModelFactory.PrepareAttributes();

            return model;
        }
        public ProductAttributeValueListModel PrepareProductAttributeValueListModel(ProductAttributeValueSearchModel searchModel)
        {
            var productAttributeValues = _productAttributeValueService.List(new ProductAttributeValueListVM
            {
                ProductAttributeMappingId = searchModel.ProductAttributeMappingId,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            if (productAttributeValues == null)
            {
                return null;
            }

            var model = new ProductAttributeValueListModel().PrepareToGrid(searchModel, productAttributeValues, () =>
            {
                return productAttributeValues.Select(productAttributeValue =>
                {
                    var productAttributeValueModel = productAttributeValue.ToModel<ProductAttributeValueModel, Guid>();

                    return productAttributeValueModel;
                });
            });

            return model;
        }
        public ProductAttributeValueModel PrepareProductAttributeValueModel(ProductAttributeValueModel model, ProductAttributeMapping productAttributeMapping, ProductAttributeValue productAttributeValue)
        {
            if (productAttributeMapping == null)
            {
                throw new ArgumentNullException(nameof(productAttributeMapping));
            }

            if (productAttributeValue != null)
            {
                model = productAttributeValue.ToModel<ProductAttributeValueModel, Guid>();
            }

            model.ProductAttributeMappingId = productAttributeMapping.Id;

            return model;
        }
        public ProductMediaListModel PrepareProductMediaListModel(ProductMediaSearchModel searchModel)
        {
            var productMediaList = _productMediaService.List(new ProductMediaListVM
            {
                ProductId = searchModel.ProductId
            });

            if(productMediaList == null)
            {
                return null;
            }

            var model = new ProductMediaListModel().PrepareToGrid(searchModel, productMediaList, () =>
            {
                return productMediaList.Select(productMedia =>
                {
                    var productMediaModel = productMedia.ToModel<ProductMediaModel, Guid>();

                    var mediaResult = _mediaService.GetById(productMedia.MediaId);
                    if (mediaResult == null)
                        throw new Exception(nameof(mediaResult));

                    productMediaModel.AltAttribute = mediaResult.AltAttribute;
                    productMediaModel.Name = mediaResult.Name;

                    return productMediaModel;
                });
            });

            return model;
        }
        public RelatedProductListModel PrepareRelatedProductListModel(RelatedProductSearchModel searchModel)
        {
            var relatedProducts = _relatedProductService.List(new RelatedProductListVM
            {
                ProductId1 = searchModel.ProductId1,
            });

            if(relatedProducts == null)
            {
                return null;
            }

            var models = new RelatedProductListModel().PrepareToGrid(searchModel, relatedProducts, () =>
            {
                return relatedProducts.Select(relatedProduct =>
                {
                    var relatedProductModel = relatedProduct.ToModel<RelatedProductModel, Guid>();

                    relatedProductModel.ProductName2 = _productService.GetById(relatedProduct.ProductId2)?.Name;

                    return relatedProductModel;
                });
            });

            return models;
        }
        public RelatedProductModel PrepareRelatedProductModel(RelatedProductModel model , Product product)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if(product == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.ProductId1 = product.Id;

            var relatedProducts = _relatedProductService.List(new RelatedProductListVM
            {
                ProductId1 = product.Id
            });

            if(relatedProducts == null || relatedProducts.Count == 0)
            {
                model.Priority = 1;
            }
            else
            {
                var Priority = relatedProducts.Max(r => r.Priority);
                model.Priority = ++Priority;
            }

            return model;
        }
        public ProductSearchModel PrepareProductSearchModel(ProductSearchModel searchModel)
        {

            searchModel.SetPopupGridPageSize();

            return searchModel;
        }
        #endregion

        #region Utilities
        protected ProductAttributeMappingSearchModel PrepareProductAttributeMappingSearchModel(ProductAttributeMappingSearchModel searchModel, Product product)
        {
            if (searchModel == null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            searchModel.ProductId = product.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }
        protected ProductAttributeValueSearchModel PrepareProductAttributeValueSearchModel(ProductAttributeValueSearchModel searchModel, ProductAttributeMapping productAttributeMapping)
        {
            if (productAttributeMapping == null)
            {
                throw new ArgumentNullException(nameof(productAttributeMapping));
            }

            searchModel.ProductAttributeMappingId = productAttributeMapping.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }
        protected ProductMediaSearchModel PrepareProductMediaSearchModel(ProductMediaSearchModel searchModel , Product product)
        {
            if (searchModel == null)
                throw new Exception(nameof(searchModel));

            if (product == null)
                throw new Exception(nameof(product));

            searchModel.ProductId = product.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }
        /// <summary>
        /// prepare related product search model
        /// </summary>
        /// <param name="searchModel">related product search model</param>
        /// <param name="product">product</param>
        /// <returns></returns>
        protected RelatedProductSearchModel PrepareRelatedProductSearchModel(RelatedProductSearchModel searchModel , Product product)
        {
            if(searchModel == null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            searchModel.ProductId1 = product.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }
        #endregion
    }
}
