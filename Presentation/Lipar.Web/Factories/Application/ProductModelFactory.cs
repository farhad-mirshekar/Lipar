using Lipar.Core;
using Lipar.Core.ReadingTime;
using Lipar.Entities;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.Enums;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Framework.Models;
using Lipar.Web.Models.Application;
using Lipar.Web.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Factories.Application
{
    public class ProductModelFactory : IProductModelFactory
    {
        #region Ctor
        public ProductModelFactory(IProductService productService
                                 , IMediaService mediaService
                                 , IProductMediaService productMediaService
                                 , IProductAttributeMappingService productAttributeMappingService
                                 , IProductAttributeValueService productAttributeValueService
                                 , IRelatedProductService relatedProductService
                                 , ICategoryService categoryService
                                 , IShippingCostService shippingCostService
                                 , IDeliveryDateService deliveryDateService
                                 , IProductCommentService productCommentService)
        {
            _productService = productService;
            _mediaService = mediaService;
            _productMediaService = productMediaService;
            _productAttributeMappingService = productAttributeMappingService;
            _productAttributeValueService = productAttributeValueService;
            _relatedProductService = relatedProductService;
            _categoryService = categoryService;
            _shippingCostService = shippingCostService;
            _deliveryDateService = deliveryDateService;
            _productCommentService = productCommentService;
        }
        #endregion

        #region fields
        private readonly IProductService _productService;
        private readonly IMediaService _mediaService;
        private readonly IProductMediaService _productMediaService;
        private readonly IProductAttributeMappingService _productAttributeMappingService;
        private readonly IProductAttributeValueService _productAttributeValueService;
        private readonly IRelatedProductService _relatedProductService;
        private readonly ICategoryService _categoryService;
        private readonly IShippingCostService _shippingCostService;
        private readonly IDeliveryDateService _deliveryDateService;
        private readonly IProductCommentService _productCommentService;
        #endregion

        #region Methods
        public ProductListModel LatestProducts(int PageIndex = 0, int PageSize = 5)
        {
            var products = _productService.List(new ProductListVM
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                //ProductSortingType = ProductSortingType.CreationDateDesc,
            });

            if (products == null)
            {
                return null;
            }

            var productListModel = new ProductListModel();

            var productsModel = products.Select(product =>
            {
                var productModel = new ProductModel();
                productModel.Id = product.Id;

                productModel.Name = product.Name;
                productModel.Price = product.Price;
                productModel.CallForPrice = product.CallForPrice;
                productModel.DiscountTypeId = product.DiscountTypeId;

                var productMedias = _productMediaService.List(new ProductMediaListVM
                {
                    ProductId = product.Id
                });

                //if (productMedias != null)
                //{
                foreach (var productMedia in productMedias)
                {
                    var mediaModel = new MediaModel
                    {
                        Id = productMedia.Id,
                        Priority = productMedia.Priority,
                        MediaId = productMedia.MediaId,
                    };

                    var media = _mediaService.GetById(productMedia.MediaId);

                    mediaModel.Name = media.Name;
                    mediaModel.AltAttribute = media.AltAttribute;
                    mediaModel.MimeType = media.MimeType;

                    productModel.Pictures.AvailableMedia.Add(mediaModel);
                }
                //}
                return productModel;
            });

            //all product
            productListModel.AvailableProducts = productsModel;

            //pagging
            productListModel.PagingInfo = new PagingInfo
            {
                CurrentPage = PageIndex == 0 ? 1 : PageIndex,
                TotalItems = products.TotalCount,
                ItemsPerPage = 2,
                Route = "Product",
            };


            return productListModel;
        }

        public ProductModel PrepareProductModel(ProductModel model, Product product, bool showComment)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (product == null)
                throw new ArgumentNullException(nameof(product));

            #region mapping
            model.Id = product.Id;
            model.Name = product.Name;
            model.AllowCustomerReviews = model.AllowCustomerReviews;
            model.CallForPrice = product.CallForPrice;
            model.CreationDate = product.CreationDate;
            model.Discount = product.Discount;
            model.DiscountTypeId = product.DiscountTypeId;
            model.FullDescription = product.FullDescription;
            model.Height = product.Height;
            model.IsDownload = product.IsDownload;
            model.Length = product.Length;
            model.MetaDescription = product.MetaDescription;
            model.MetaKeywords = product.MetaKeywords;
            model.MetaTitle = product.MetaTitle;
            model.Price = product.Price;
            model.Published = product.Published;
            model.ShortDescription = product.ShortDescription;
            model.CreationDate = product.CreationDate;
            model.UpdatedDate = product.UpdatedDate;
            #endregion

            //prepare product attributes
            model.ProductAttributes = PrepareProductAttributeModel(product);

            //prepare related product
            model.RelatedProducts = PrepareRelatedProductModel(product);

            //prepare pictures
            model.Pictures = PrepareMediaListModel(product);

            //prepare category
            var category = _categoryService.GetById(product.CategoryId);
            model.Category = PrepareCategoryModel(category);

            //prepare delivery date
            var deliveryDate = _deliveryDateService.GetById(product.DeliveryDateId ?? 0);
            model.DeliveryDate = PrepareDeliveryDateModel(deliveryDate);

            //prepare shipping cost
            var shippingCost = _shippingCostService.GetById(product.ShippingCostId ?? 0);
            model.ShippingCost = PrepareShippingCostModel(shippingCost);

            //prepare product comment model for add comment
            model.ProductCommentModel = PrepareProductCommentModel(product);
            
            //prepare product questions
            //model.ProductQuestions = PrepareProductQuestionListModel(product);

            if (showComment)
            {
                //get all product comments
                var productComments = _productCommentService.List(new ProductCommentListVM
                {
                    ProductId = model.Id,
                    CommentStatusId =(int)CommentStatusEnum.Open,
                });

                //prepare product comments list
                model.ProductComments = PrepareProductCommentListModel(productComments);
            }

            return model;
        }

        public IList<ProductCommentListModel> PrepareProductCommentListModel(IList<ProductComment> productComments)
        {
            var productCommentList = new List<ProductCommentListModel>();

            foreach (var productComment in productComments.Where(pc => pc.ParentId == null))
            {
                var productCommentModel = new ProductCommentListModel
                {
                    Id = productComment.Id,
                    ProductId = productComment.ProductId,
                    CommentText = productComment.CommentText,
                    CreationDate = productComment.CreationDate,
                    FullName = $"{productComment.User.FirstName} {productComment.User.LastName}",
                    ParentId = productComment.ParentId,
                    ReplyText = productComment.ReplyText,
                };

                if (productComment.Children != null && productComment.Children.Count() > 0)
                {
                    foreach (var comment in productComment.Children)
                    {
                        productCommentModel.Children = PrepareProductCommentChildren(productComments.ToList(), comment);
                    }
                }

                productCommentList.Add(productCommentModel);
            }

            return productCommentList;
        }

        public IList<ProductQuestionModel> PrepareProductQuestionListModel(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var productQuestions = new List<ProductQuestionModel>();

            if (product.ProductQuestions.Count() > 0)
            {
                foreach (var productQuestion in product.ProductQuestions.OrderByDescending(pq=>pq.CreationDate))
                {
                    var productQuestionModel = new ProductQuestionModel();
                    productQuestionModel.Id = productQuestion.Id;
                    productQuestionModel.QuestionText = productQuestion.QuestionText;
                    productQuestionModel.ProductId = productQuestion.ProductId;
                    productQuestionModel.FullName = $"{productQuestion.User.FirstName} {productQuestion.User.LastName}";
                    productQuestionModel.CreationDate = productQuestion.CreationDate;

                    productQuestionModel.ProductAnswers = PrepareProductAnswersListModel(productQuestion.ProductAnswers.ToList());

                    //add to list
                    productQuestions.Add(productQuestionModel);
                }
            }

            return productQuestions;
        }

        public ProductQuestionModel PrepareProductQuestionModel(ProductQuestionModel model,Product product)
        {
            if(product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            model.ProductId = product.Id;

            return model;
        }
        #endregion

        #region Utilities
        /// <summary>
        /// prepare product attribute model
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        protected IList<ProductAttributeModel> PrepareProductAttributeModel(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var model = new List<ProductAttributeModel>();
            var productAttributeMappings = _productAttributeMappingService.List(new ProductAttributeMappingListVM { ProductId = product.Id });

            foreach (var productAttributeMapping in productAttributeMappings)
            {
                var attributeModel = new ProductAttributeModel
                {
                    Id = productAttributeMapping.Id,
                    TextPrompt = productAttributeMapping.TextPrompt,
                    ProductId = product.Id,
                    ProductAttributeId = productAttributeMapping.ProductAttributeId,
                    IsRequired = productAttributeMapping.IsRequired,
                    AttributeControlTypeId = productAttributeMapping.AttributeControlTypeId,
                    CreationDate = productAttributeMapping.CreationDate,
                };


                if (productAttributeMapping.AttributeControlTypeId == (int)AttributeControlTypeEnum.Dropdown)
                {
                    var productAttributeValues = _productAttributeValueService.List(new ProductAttributeValueListVM { ProductAttributeMappingId = productAttributeMapping.Id });
                    foreach (var productAttributeValue in productAttributeValues)
                    {
                        var valueModel = new ProductAttributeValueModel
                        {
                            Id = productAttributeValue.Id,
                            CreationDate = productAttributeValue.CreationDate,
                            IsPreSelected = productAttributeValue.IsPreSelected,
                            Name = productAttributeValue.Name,
                            Price = productAttributeValue.Price,
                        };

                        attributeModel.ProductAttributeValues.Add(valueModel);
                    }
                }

                model.Add(attributeModel);
            }

            return model;
        }
        /// <summary>
        /// prepare related product model
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        protected IList<RelatedProductModel> PrepareRelatedProductModel(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var relatedProductsModel = new List<RelatedProductModel>();
            var relatedProducts = _relatedProductService.GetRelatedProductsByProductId1(product.Id);

            foreach (var relatedProduct in relatedProducts)
            {
                var product2 = _productService.GetById(relatedProduct.ProductId2);

                var productMedias = _productMediaService.List(new ProductMediaListVM
                {
                    ProductId = relatedProduct.ProductId2
                });

                var mediaModel = new MediaModel();

                if (productMedias != null && productMedias.Count > 0)
                {
                    var productMedia = productMedias.OrderByDescending(pm => pm.Priority).FirstOrDefault();

                    var media = _mediaService.GetById(productMedia.MediaId);

                    mediaModel.Id = productMedia.Id;
                    mediaModel.Priority = productMedia.Priority;
                    mediaModel.Name = media.Name;
                    mediaModel.AltAttribute = media.AltAttribute;
                    mediaModel.MimeType = media.MimeType;
                    mediaModel.MediaId = media.Id;
                }

                relatedProductsModel.Add(new RelatedProductModel
                {
                    Id = relatedProduct.Id,
                    CreationDate = relatedProduct.CreationDate,
                    MediaModel = mediaModel,
                    Priority = relatedProduct.Priority,
                    ProductId2 = relatedProduct.ProductId2,
                    ProductName2 = product2.Name,
                });
            }

            return relatedProductsModel;
        }
        /// <summary>
        /// prepare media list model
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        protected MediaListModel PrepareMediaListModel(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var mediaListModel = new MediaListModel();
            var productMedias = _productMediaService.List(new ProductMediaListVM
            {
                ProductId = product.Id
            });

            foreach (var productMedia in productMedias)
            {
                var media = _mediaService.GetById(productMedia.MediaId);

                mediaListModel.AvailableMedia.Add(new MediaModel
                {
                    Id = productMedia.Id,
                    Priority = productMedia.Priority,
                    Name = media.Name,
                    AltAttribute = media.AltAttribute,
                    MimeType = media.MimeType,
                    CreationDate = media.CreationDate,
                    MediaId = productMedia.MediaId,
                });
            }

            return mediaListModel;
        }

        /// <summary>
        /// prepare category model
        /// </summary>
        /// <param name="category">category</param>
        /// <returns></returns>
        protected CategoryModel PrepareCategoryModel(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var categoryModel = new CategoryModel
            {
                Id = category.Id,
                CreationDate = category.CreationDate,
                Description = category.Description,
                EnabledTypeId = category.EnabledTypeId,
                IncludeInTopMenu = category.IncludeInTopMenu,
                MetaDescription = category.MetaDescription,
                Name = category.Name,
                ParentId = category.ParentId,
                NameCrumb = _categoryService.GetFormattedBreadCrumb(category),
            };

            return categoryModel;
        }

        /// <summary>
        /// prepare delivery date model
        /// </summary>
        /// <param name="deliveryDate">delivery date</param>
        /// <returns></returns>
        protected DeliveryDateModel PrepareDeliveryDateModel(DeliveryDate deliveryDate)
        {
            if (deliveryDate == null)
            {
                return null;
            }

            var deliveryDateModel = new DeliveryDateModel
            {
                Id = deliveryDate.Id,
                CreationDate = deliveryDate.CreationDate,
                Description = deliveryDate.Description,
                EnabledTypeId = deliveryDate.EnabledTypeId,
                Name = deliveryDate.Name,
                Priority = deliveryDate.Priority,
            };

            return deliveryDateModel;
        }

        /// <summary>
        /// prepare shipping cost model
        /// </summary>
        /// <param name="shippingCost"></param>
        /// <returns></returns>
        protected ShippingCostModel PrepareShippingCostModel(ShippingCost shippingCost)
        {
            if (shippingCost == null)
            {
                return null;
            }

            var shippingCostModel = new ShippingCostModel
            {
                Id = shippingCost.Id,
                CreationDate = shippingCost.CreationDate,
                Description = shippingCost.Description,
                EnabledTypeId = shippingCost.EnabledTypeId,
                Name = shippingCost.Name,
                Price = shippingCost.Price,
            };

            return shippingCostModel;
        }

        /// <summary>
        /// prepare product comment model
        /// </summary>
        /// <param name="model">product</param>
        /// <returns></returns>
        protected ProductCommentModel PrepareProductCommentModel(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var productCommentModel = new ProductCommentModel();
            productCommentModel.ProductId = model.Id;
            productCommentModel.Slug = CalculateWordsCount.CleanSeoUrl(model.Name);

            return productCommentModel;
        }
        /// <summary>
        /// prepare product comment children
        /// </summary>
        /// <param name="productComments">all product comment list</param>
        /// <param name="productComment">children</param>
        /// <returns></returns>
        protected IList<ProductCommentListModel> PrepareProductCommentChildren(List<ProductComment> productComments, ProductComment productComment)
        {
            var foundChildren = productComments.Where(pc => pc.ParentId == productComment.Id);
            var productCommentModel = new List<ProductCommentListModel>();

            if (foundChildren.Count() > 0)
            {
                foreach (var comment in foundChildren)
                {
                    productCommentModel.Add(new ProductCommentListModel
                    {
                        Id = productComment.Id,
                        ProductId = productComment.ProductId,
                        CommentText = productComment.CommentText,
                        CreationDate = productComment.CreationDate,
                        FullName = $"{productComment.User.FirstName} {productComment.User.LastName}",
                        ParentId = productComment.ParentId,
                        ReplyText = productComment.ReplyText,
                        Children = PrepareProductCommentChildren(productComments, comment)
                    });
                }
            }

            else
            {
                productCommentModel.Add(new ProductCommentListModel
                {
                    Id = productComment.Id,
                    ProductId = productComment.ProductId,
                    CommentText = productComment.CommentText,
                    CreationDate = productComment.CreationDate,
                    FullName = $"{productComment.User.FirstName} {productComment.User.LastName}",
                    ParentId = productComment.ParentId,
                });

            }

            return productCommentModel;
        }

        /// <summary>
        /// prepare product answer list model
        /// </summary>
        /// <param name="productAnswers">product answers</param>
        /// <returns></returns>
        protected IList<ProductAnswersModel> PrepareProductAnswersListModel(IList<ProductAnswers> productAnswers)
        {
            var productAnswersModel = new List<ProductAnswersModel>();
            foreach (var productAnswer in productAnswers.OrderByDescending(pa => pa.CreationDate))
            {
                var productAnswerModel = new ProductAnswersModel();
                productAnswerModel.Id = productAnswer.Id;
                productAnswerModel.AnswerText = productAnswer.AnswerText;
                productAnswerModel.ProductQuestionId = productAnswer.ProductQuestionId;
                productAnswer.CreationDate = productAnswer.CreationDate;
                productAnswerModel.FullName = $"{productAnswer.User.FirstName} {productAnswer.User.LastName}";

                productAnswersModel.Add(productAnswerModel);
            }

            return productAnswersModel;
        }
        #endregion
    }
}
