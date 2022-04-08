using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Services.Notification;
using Lipar.Web.Areas.Admin.Factories.Application;
using Lipar.Web.Areas.Admin.Helpers;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class ProductTagController : BaseAdminController
    {
        #region Ctor
        public ProductTagController(IProductTagService productTagService
                                  , IProductTagModelFactory productTagModelFactory
                                  , ILocaleStringResourceService localeStringResourceService
                                  , INotificationService notificationService)
        {
            _productTagService = productTagService;
            _productTagModelFactory = productTagModelFactory;
            _localeStringResourceService = localeStringResourceService;
            _notificationService = notificationService;
        }
        #endregion

        #region Fields
        private readonly IProductTagService _productTagService;
        private readonly IProductTagModelFactory _productTagModelFactory;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly INotificationService _notificationService;
        #endregion

        #region Methods
        [CheckingPermissions(permissions: CommandNames.ManageProductTag)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageProductTag)]
        public IActionResult List()
            => View(new ProductTagSearchModel());

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductTag)]
        public IActionResult List(ProductTagSearchModel searchModel)
        {
            var model = _productTagModelFactory.PrepareProductTagListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProductTag)]
        public IActionResult Create()
        {
            var model = _productTagModelFactory.PrepareProductTagModel(new ProductTagModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductTag)]
        public IActionResult Create(ProductTagModel model)
        {
            if (ModelState.IsValid)
            {
                var productTag = ToEntity(model);

                //add product tag
                _productTagService.Add(productTag);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = productTag.Id });
            }

            model = _productTagModelFactory.PrepareProductTagModel(model, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageProductTag)]
        public IActionResult Edit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var productTag = _productTagService.GetById(Id);
            if (productTag == null)
            {
                return RedirectToAction("List");
            }

            var model = _productTagModelFactory.PrepareProductTagModel(new ProductTagModel(), productTag);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductTag)]
        public IActionResult Edit(ProductTagModel model)
        {
            if (ModelState.IsValid)
            {
                var productTag = ToEntity(model);

                //edit product tag
                _productTagService.Edit(productTag);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = productTag.Id });
            }

            model = _productTagModelFactory.PrepareProductTagModel(model, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageProductTag)]
        public IActionResult Delete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var productTag = _productTagService.GetById(Id);
            if (productTag == null)
            {
                return RedirectToAction("List");
            }

            //delete product tag
            _productTagService.Delete(productTag);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));

            return RedirectToAction("List");
        }
        #endregion

        #region Utilities
        private ProductTag ToEntity(ProductTagModel model)
        {
            var productTag = new ProductTag
            {
                Name = model.Name,
            };

            return productTag;
        }
        #endregion
    }
}
