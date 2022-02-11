using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.Notification;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Application;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Lipar.Web.Framework.MVC.Filters;
using Lipar.Web.Areas.Admin.Helpers;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class ProductAttributeController : BaseAdminController
    {
        #region Ctor
        public ProductAttributeController(IProductAttributeModelFactory productAttributeModelFactory
                                        , IProductAttributeService productAttributeService
                                        , ILocaleStringResourceService localeStringResourceService
                                        , IActivityLogService activityLogService
                                        , INotificationService notificationService)
        {
            _productAttributeModelFactory = productAttributeModelFactory;
            _productAttributeService = productAttributeService;
            _localeStringResourceService = localeStringResourceService;
            _activityLogService = activityLogService;
            _notificationService = notificationService;
        }
        #endregion

        #region Fields
        private readonly IProductAttributeModelFactory _productAttributeModelFactory;
        private readonly IProductAttributeService _productAttributeService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IActivityLogService _activityLogService;
        private readonly INotificationService _notificationService;
        #endregion

        #region Methods

        [CheckingPermissions(permissions: CommandNames.ManageProductAttribute)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageProductAttribute)]
        public IActionResult List()
            => View(new ProductAttributeSearchModel());

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductAttribute)]
        public IActionResult List(ProductAttributeSearchModel searchModel)
        {
            var model = _productAttributeModelFactory.PrepareProductAttributeListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProductAttribute)]
        public IActionResult Create()
        {
            var model = _productAttributeModelFactory.PrepareProductAttributeModel(new ProductAttributeModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductAttribute)]
        public IActionResult Create(ProductAttributeModel model)
        {
            if (ModelState.IsValid)
            {
                var productAttribute = model.ToEntity<ProductAttribute, Guid>();

                //add product attribute
                _productAttributeService.Add(productAttribute);

                //add activity log for create product attribute
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.ProductAttribute.Create"), productAttribute);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new {Id = productAttribute.Id });
            }

            model = _productAttributeModelFactory.PrepareProductAttributeModel(model, null);
            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProductAttribute)]
        public IActionResult Edit(Guid Id)
        {
            var productAttribute = _productAttributeService.GetById(Id);
            if(productAttribute == null)
            {
                return RedirectToAction("List");
            }

            var model = _productAttributeModelFactory.PrepareProductAttributeModel(null, productAttribute);

            return View(model);

        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductAttribute)]
        public IActionResult Edit(ProductAttributeModel model)
        {
            if (ModelState.IsValid)
            {
                var productAttribute = model.ToEntity<ProductAttribute, Guid>();

                //edit product attribute
                _productAttributeService.Edit(productAttribute);

                //add activity log for edit product attribute
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.ProductAttribute.Edit"), productAttribute);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = productAttribute.Id });
            }

            model = _productAttributeModelFactory.PrepareProductAttributeModel(model, null);
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductAttribute)]
        public IActionResult Delete(Guid Id)
        {
            var productAttribute = _productAttributeService.GetById(Id);
            if (productAttribute == null)
            {
                return RedirectToAction("List");
            }

            _productAttributeService.Delete(productAttribute);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));

            return RedirectToAction("List");
        }
        #endregion
    }
}
