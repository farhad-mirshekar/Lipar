using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Application;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class ProductAttributeController : BaseAdminController
    {
        #region Ctor
        public ProductAttributeController(IProductAttributeModelFactory productAttributeModelFactory
                                        , IProductAttributeService productAttributeService
                                        , ICommandService commandService
                                        , ILocaleStringResourceService localeStringResourceService
                                        , IActivityLogService activityLogService
                                        , INotificationService notificationService)
        {
            _productAttributeModelFactory = productAttributeModelFactory;
            _productAttributeService = productAttributeService;
            _commandService = commandService;
            _localeStringResourceService = localeStringResourceService;
            _activityLogService = activityLogService;
            _notificationService = notificationService;
        }
        #endregion

        #region Fields
        private readonly IProductAttributeModelFactory _productAttributeModelFactory;
        private readonly IProductAttributeService _productAttributeService;
        private readonly ICommandService _commandService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IActivityLogService _activityLogService;
        private readonly INotificationService _notificationService;
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new ProductAttributeSearchModel());

        [HttpPost]
        public IActionResult List(ProductAttributeSearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageProductAttribute");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _productAttributeModelFactory.PrepareProductAttributeListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            var permission = _commandService.CheckPermission("ManageProductAttribute");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _productAttributeModelFactory.PrepareProductAttributeModel(new ProductAttributeModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductAttributeModel model)
        {
            var permission = _commandService.CheckPermission("ManageProductAttribute");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var productAttribute = model.ToEntity<ProductAttribute>();

                //add product attribute
                _productAttributeService.Add(productAttribute);

                //add activity log for create product attribute
                _activityLogService.Add("Admin.ProductAttribute.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.ProductAttribute.Create"), productAttribute);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new {Id = productAttribute.Id });
            }

            model = _productAttributeModelFactory.PrepareProductAttributeModel(model, null);
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            var permission = _commandService.CheckPermission("ManageProductAttribute");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var productAttribute = _productAttributeService.GetById(Id);
            if(productAttribute == null)
            {
                return RedirectToAction("List");
            }

            var model = _productAttributeModelFactory.PrepareProductAttributeModel(null, productAttribute);

            return View(model);

        }

        [HttpPost]
        public IActionResult Edit(ProductAttributeModel model)
        {
            var permission = _commandService.CheckPermission("ManageProductAttribute");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var productAttribute = model.ToEntity<ProductAttribute>();

                //edit product attribute
                _productAttributeService.Edit(productAttribute);

                //add activity log for edit product attribute
                _activityLogService.Add("Admin.ProductAttribute.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.ProductAttribute.Edit"), productAttribute);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = productAttribute.Id });
            }

            model = _productAttributeModelFactory.PrepareProductAttributeModel(model, null);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var permission = _commandService.CheckPermission("ManageProductAttribute");
            if (!permission)
            {
                return AccessDeniedView();
            }

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
