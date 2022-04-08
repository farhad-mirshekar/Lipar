using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.General;
using Lipar.Services.Application.Contracts;
using Lipar.Services.Notification;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Application;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Lipar.Web.Framework.MVC.Filters;
using Lipar.Web.Areas.Admin.Helpers;
using Lipar.Core;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        #region Ctor
        public ProductController(IProductModelFactory productModelFactory
                               , IProductService productService
                               , ILocaleStringResourceService localeStringResourceService
                               , INotificationService notificationService
                               , IActivityLogService activityLogService
                               , IProductAttributeMappingService productAttributeMappingService
                               , IProductAttributeValueService productAttributeValueService
                               , IMediaService mediaService
                               , IProductMediaService productMediaService
                               , IUrlRecordService urlRecordService
                               , IRelatedProductService relatedProductService
                               , IProductCommentService productCommentService
                               , IProductCommentModelFactory productCommentModelFactory
                               , IProductQuestionModelFactory productQuestionModelFactory
                               , IProductQuestionService productQuestionService
                               , IProductAnswersService productAnswersService
                               , IWorkContext workContext
                               , IProductTagService productTagService)
        {
            _productModelFactory = productModelFactory;
            _productService = productService;
            _localeStringResourceService = localeStringResourceService;
            _notificationService = notificationService;
            _activityLogService = activityLogService;
            _productAttributeMappingService = productAttributeMappingService;
            _productAttributeValueService = productAttributeValueService;
            _mediaService = mediaService;
            _productMediaService = productMediaService;
            _urlRecordService = urlRecordService;
            _relatedProductService = relatedProductService;
            _productCommentService = productCommentService;
            _productCommentModelFactory = productCommentModelFactory;
            _productQuestionModelFactory = productQuestionModelFactory;
            _productQuestionService = productQuestionService;
            _productAnswersService = productAnswersService;
            _workContext = workContext;
            _productTagService = productTagService;
        }
        #endregion

        #region Fields
        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly INotificationService _notificationService;
        private readonly IActivityLogService _activityLogService;
        private readonly IProductAttributeMappingService _productAttributeMappingService;
        private readonly IProductAttributeValueService _productAttributeValueService;
        private readonly IMediaService _mediaService;
        private readonly IProductMediaService _productMediaService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IRelatedProductService _relatedProductService;
        private readonly IProductCommentService _productCommentService;
        private readonly IProductCommentModelFactory _productCommentModelFactory;
        private readonly IProductQuestionModelFactory _productQuestionModelFactory;
        private readonly IProductQuestionService _productQuestionService;
        private readonly IProductAnswersService _productAnswersService;
        private readonly IWorkContext _workContext;
        private readonly IProductTagService _productTagService;
        #endregion

        #region Product Methods

        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult List()
            => View(new ProductSearchModel());

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult List(ProductSearchModel searchModel)
        {
            var model = _productModelFactory.PrepareProductListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult Create()
        {
            var model = _productModelFactory.PrepareProductModel(new ProductModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = model.ToEntity<Product, Guid>();

                //add product 
                _productService.Add(product);

                //add url record
                _urlRecordService.SaveSlug<Product, Guid>(product, product.Name, _workContext.WorkingLanguage.Id);

                //add activity log for create product
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.Product.Create"), product);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = product.Id });
            }

            model = _productModelFactory.PrepareProductModel(model, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult Edit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var product = _productService.GetById(Id);
            if (product is null)
            {
                return RedirectToAction("List");
            }

            //check product not remove
            if (product.RemoverId.HasValue && product.RemoverId.Value != Guid.Empty)
            {
                //notification error
                _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Notification.Error.EntityRemove"));

                return RedirectToAction("List");
            }

            var model = _productModelFactory.PrepareProductModel(null, product);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = model.ToEntity<Product, Guid>();

                //edit product 
                _productService.Edit(product);

                if (model.ProductTags.Any())
                {
                    //save product tag
                    _productTagService.SaveProductTagMapping(model.ProductTags, model.Id);
                }
                
                //add url record
                _urlRecordService.SaveSlug<Product, Guid>(product, product.Name, _workContext.WorkingLanguage.Id);

                //add activity log for edit product
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Product.Edit"), product);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = product.Id });
            }

            model = _productModelFactory.PrepareProductModel(model, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult Delete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var product = _productService.GetById(Id);
            if (product == null)
            {
                return RedirectToAction("List");
            }

            _productService.Delete(product);

            //add activity log for delete product
            _activityLogService.Add("Admin.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.Product.Delete"), product);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));

            return RedirectToAction("List");
        }
        #endregion

        #region Product Attribute Mapping Methods

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeMappingList(ProductAttributeMappingSearchModel searchModel)
        {
            var product = _productService.GetById(searchModel.ProductId) ?? throw new ArgumentException("product not found");
            if (product.RemoverId.HasValue && product.RemoverId.Value != Guid.Empty)
            {
                return RedirectToAction("List");
            }

            //prepare model
            var model = _productModelFactory.PrepareProductAttributeMappingListModel(searchModel, product);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeMappingCreate(Guid ProductId)
        {
            var product = _productService.GetById(ProductId) ?? throw new ArgumentException("product not found");
            if (product.RemoverId.HasValue && product.RemoverId.Value != Guid.Empty)
            {
                return RedirectToAction("List");
            }

            //prepare model 
            var model = _productModelFactory.PrepareProductAttributeMappingModel(new ProductAttributeMappingModel(), product, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeMappingCreate(ProductAttributeMappingModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetById(model.ProductId) ?? throw new ArgumentException("product not found");
                if (product.RemoverId.HasValue && product.RemoverId.Value != Guid.Empty)
                {
                    return RedirectToAction("List");
                }

                var productAttributeMappings = _productAttributeMappingService.List(new ProductAttributeMappingListVM
                {
                    ProductId = model.ProductId
                });

                if (productAttributeMappings.Any(p => p.ProductAttributeId == model.ProductAttributeId))
                {
                    _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Product.ProductAttributes.Attributes.AlreadyExists"));

                    model = _productModelFactory.PrepareProductAttributeMappingModel(model, product, null);
                    return View(model);
                }

                var productAttributeMapping = model.ToEntity<ProductAttributeMapping, Guid>();

                //add product attribute mapping
                _productAttributeMappingService.Add(productAttributeMapping);

                return RedirectToAction("ProductAttributeMappingEdit", new { Id = productAttributeMapping.Id });
            }

            model = _productModelFactory.PrepareProductAttributeMappingModel(model, null, null);
            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeMappingEdit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            //try to get a product attribute mapping with the specified id
            var productAttributeMapping = _productAttributeMappingService.GetById(Id)
                ?? throw new ArgumentException("No product attribute mapping found with the specified id");

            //try to get a product with the specified id
            var product = _productService.GetById(productAttributeMapping.ProductId)
                ?? throw new ArgumentException("No product found with the specified id");

            if (product.RemoverId.HasValue && product.RemoverId.Value != Guid.Empty)
            {
                //notification error
                _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Notification.Error.EntityRemove"));

                return RedirectToAction("List");
            }

            var model = _productModelFactory.PrepareProductAttributeMappingModel(null, product, productAttributeMapping);
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeMappingEdit(ProductAttributeMappingModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetById(model.ProductId) ?? throw new ArgumentException("product not found");
                if (product.RemoverId.HasValue && product.RemoverId.Value != Guid.Empty)
                {
                    return RedirectToAction("List");
                }

                var productAttributeMappings = _productAttributeMappingService.List(new ProductAttributeMappingListVM
                {
                    ProductId = model.ProductId
                });

                if (productAttributeMappings.Any(p => p.ProductAttributeId == model.ProductAttributeId))
                {
                    _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Product.ProductAttributes.Attributes.AlreadyExists"));

                    model = _productModelFactory.PrepareProductAttributeMappingModel(model, product, null);
                    return View(model);
                }

                var productAttributeMapping = model.ToEntity<ProductAttributeMapping, Guid>();

                //edit product attribute mapping
                _productAttributeMappingService.Edit(productAttributeMapping);

                return RedirectToAction("ProductAttributeMappingEdit", new { Id = productAttributeMapping.Id });
            }

            model = _productModelFactory.PrepareProductAttributeMappingModel(model, null, null);
            return View(model);
        }
        #endregion

        #region Product Attribute Value Methods

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeValueList(ProductAttributeValueSearchModel searchModel)
        {
            var model = _productModelFactory.PrepareProductAttributeValueListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeValueCreatePopup(Guid ProductAttributeMappingId)
        {
            if (ProductAttributeMappingId == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var productAttributeMapping = _productAttributeMappingService.GetById(ProductAttributeMappingId)
                ?? throw new ArgumentException("No product attribute mapping found with the specified id");

            var model = _productModelFactory.PrepareProductAttributeValueModel(new ProductAttributeValueModel(), productAttributeMapping, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeValueCreatePopup(ProductAttributeValueModel model)
        {
            var productAttributeMapping = _productAttributeMappingService.GetById(model.ProductAttributeMappingId)
                ?? throw new ArgumentException("No product attribute mapping found with the specified id");

            if (ModelState.IsValid)
            {
                var productAttributeValue = model.ToEntity<ProductAttributeValue, Guid>();

                _productAttributeValueService.Add(productAttributeValue);

                ViewBag.RefreshPage = true;

                return View(model);
            }

            model = _productModelFactory.PrepareProductAttributeValueModel(model, productAttributeMapping, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeValueEditPopup(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var productAttributeValue = _productAttributeValueService.GetById(Id)
                ?? throw new ArgumentException("No product attribute value found with specified id");

            var productAttributeMapping = _productAttributeMappingService.GetById(productAttributeValue.ProductAttributeMappingId)
                ?? throw new ArgumentException("No product attribute mapping found with specified id");

            var model = _productModelFactory.PrepareProductAttributeValueModel(null, productAttributeMapping, productAttributeValue);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductAttributeValueEditPopup(ProductAttributeValueModel model)
        {
            var productAttributeMapping = _productAttributeMappingService.GetById(model.ProductAttributeMappingId)
                ?? throw new ArgumentException("No product attribute mapping found with the specified id");

            if (ModelState.IsValid)
            {
                var productAttributeValue = model.ToEntity<ProductAttributeValue, Guid>();

                _productAttributeValueService.Edit(productAttributeValue);

                ViewBag.RefreshPage = true;

                return View(model);
            }

            model = _productModelFactory.PrepareProductAttributeValueModel(model, productAttributeMapping, null);

            return View(model);
        }
        #endregion

        #region Product Media Methods

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductMediaList(ProductMediaSearchModel searchModel)
        {
            var model = _productModelFactory.PrepareProductMediaListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductMediaCreate(ProductMediaModel model)
        {
            _mediaService.EditPicture(new Media
            {
                Id = model.MediaId,
                AltAttribute = model.AltAttribute,
                Name = model.Name,
            });

            _productMediaService.Save(new ProductMedia
            {
                ProductId = model.ProductId,
                MediaId = model.MediaId,
                Priority = model.Priority,
            });

            return Json(new { Result = true });
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductMediaEdit(ProductMediaModel model)
        {
            var productMedia = _productMediaService.GetById(model.Id)
                ?? throw new ArgumentNullException("no product media found with the specified id");

            productMedia.Priority = model.Priority;

            _productMediaService.Save(productMedia);

            var media = _mediaService.GetById(productMedia.MediaId)
                ?? throw new ArgumentNullException("no media found with the specified id");

            media.AltAttribute = model.AltAttribute;
            media.Name = media.Name;

            _mediaService.EditPicture(media);

            return new NullJsonResult();
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult ProductMediaDelete(Guid Id)
        {
            var media = _mediaService.GetById(Id)
                ?? throw new ArgumentNullException("no media found with the specified id");

            var productMedia = _productMediaService.GetByMediaId(Id)
                ?? throw new ArgumentNullException("no media found with the specified id");

            _productMediaService.Delete(productMedia);

            _mediaService.Delete(media);

            return new NullJsonResult();
        }
        #endregion

        #region Related Product Methods

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult RelatedProductList(RelatedProductSearchModel searchModel)
        {
            var model = _productModelFactory.PrepareRelatedProductListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult RelatedProductCreatePopup(int ProductId)
        {
            var model = _productModelFactory.PrepareProductSearchModel(new ProductSearchModel());

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult RelatedProductCreatePopup(AddRelatedProductModel model)
        {
            var selectedProducts = _productService.GetByIds(model.SelectedProductIds.ToArray());
            if (selectedProducts.Any())
            {
                var existingRelatedProducts = _relatedProductService.GetRelatedProductsByProductId1(model.ProductId);
                foreach (var product in selectedProducts)
                {
                    if (_relatedProductService.FindRelatedProduct(existingRelatedProducts, model.ProductId, product.Id) != null)
                    {
                        continue;
                    }

                    _relatedProductService.Add(new RelatedProduct
                    {
                        ProductId1 = model.ProductId,
                        ProductId2 = product.Id,
                        Priority = 1
                    });
                }
            }

            ViewBag.RefreshPage = true;

            return View(new ProductSearchModel());
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProduct)]
        public IActionResult RelatedProductCreatePopupList(ProductSearchModel searchModel)
        {
            var model = _productModelFactory.PrepareProductListModel(searchModel);

            return Json(model);
        }

        #endregion

        #region Product Comment Methods

        [CheckingPermissions(permissions: CommandNames.ManageProductComment)]
        public IActionResult ProductCommentList()
        {
            var searchModel = _productCommentModelFactory.PrepareProductCommentSearchModel();
            return View(searchModel);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductComment)]
        public IActionResult ProductCommentList(ProductCommentSearchModel searchModel)
        {
            var model = _productCommentModelFactory.PrepareProductCommentListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProductComment)]
        public IActionResult ProductCommentEdit(Guid Id)
        {
            var productComment = _productCommentService.GetById(Id);
            if (productComment is null)
            {
                return RedirectToAction("List");
            }

            var model = _productCommentModelFactory.PrepareProductCommentModel(new ProductCommentModel(), productComment);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductComment)]
        public IActionResult ProductCommentEdit(ProductCommentModel model)
        {
            if (ModelState.IsValid)
            {
                var productComment = _productCommentService.GetById(model.Id);

                productComment.ReplyText = model.ReplyText;
                if (!productComment.CommentText.Equals(model.CommentText))
                {
                    productComment.CommentText = model.CommentText;
                }

                //edit product comment
                _productCommentService.Edit(productComment);

                //add activity log for edit product comment
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.ProductComment.Edit"), productComment);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));
            }

            model = _productCommentModelFactory.PrepareProductCommentModel(model, null);
            return View(model);
        }
        #endregion

        #region Product Question Methods

        [CheckingPermissions(permissions: CommandNames.ManageProductQuestion)]
        public IActionResult ProductQuestionList(Guid? filterByProductId)
        {
            var product = _productService.GetById(filterByProductId.Value, true);

            if (product == null && (filterByProductId.HasValue && filterByProductId.Value != Guid.Empty))
            {
                return RedirectToAction("ProductQuestionList");
            }

            var model = _productQuestionModelFactory.PrepareProductQuestionSearchModel(new ProductQuestionSearchModel(), product);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductQuestion)]
        public IActionResult ProductQuestionList(ProductQuestionSearchModel searchModel)
        {
            var model = _productQuestionModelFactory.PrepareProductQuestionListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProductQuestion)]
        public IActionResult ProductQuestionEdit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("ProductQuestionList");
            }

            var productQuestion = _productQuestionService.GetById(Id);
            if (productQuestion == null)
            {
                return RedirectToAction("ProductQuestionList");
            }

            var model = _productQuestionModelFactory.PrepareProductQuestionModel(null, productQuestion);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductQuestion)]
        public IActionResult ProductQuestionEdit(ProductQuestionModel model)
        {
            if (ModelState.IsValid)
            {
                var productQuestion = _productQuestionService.GetById(model.Id);
                if (productQuestion == null)
                {

                }

                productQuestion.QuestionText = model.QuestionText;
                productQuestion.ViewStatusId = model.ViewStatusId;

                _productQuestionService.Edit(productQuestion);
            }
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductQuestion)]
        public IActionResult ProductAnswersList(ProductAnswersSearchModel searchModel)
        {
            if (searchModel != null && searchModel.ProductQuestionId.HasValue && searchModel.ProductQuestionId.Value != Guid.Empty)
            {
                var productQuestion = _productQuestionService.GetById(searchModel.ProductQuestionId.Value, true);
                if (productQuestion == null)
                {
                    return RedirectToAction("ProductQuestionList");
                }
            }

            var model = _productQuestionModelFactory.PrepareProductAnswersListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProductQuestion)]
        public IActionResult ProductAnswersEdit(Guid Id)
        {
            var productAnswer = _productAnswersService.GetById(Id, true);
            if (productAnswer == null)
            {
                return RedirectToAction("ProductQuestionList");
            }

            var model = _productQuestionModelFactory.PrepareProductAnswersModel(new ProductAnswersModel(), productAnswer);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductQuestion)]
        public IActionResult ProductAnswersEdit(ProductAnswersModel model)
        {
            if (ModelState.IsValid)
            {
                var productAnswers = model.ToEntity<ProductAnswers, Guid>();

                _productAnswersService.Edit(productAnswers);

                //add activity log for edit product answer
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.ProductAnswers.Edit"), productAnswers);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("ProductQuestionEdit", new { Id = productAnswers.ProductQuestionId });
            }

            model = _productQuestionModelFactory.PrepareProductAnswersModel(model, null);

            return View(model);
        }
        #endregion
    }
}
