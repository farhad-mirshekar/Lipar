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
using System;
using Lipar.Web.Framework.MVC.Filters;
using Lipar.Web.Areas.Admin.Helpers;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class DeliveryDateController : BaseAdminController
    {
        #region Ctor
        public DeliveryDateController(IDeliveryDateModelFactory deliveryDateModelFactory
                                    , IDeliveryDateService deliveryDateService
                                    , ICommandService commandService
                                    , ILocaleStringResourceService localeStringResourceService
                                    , IActivityLogService activityLogService
                                    , INotificationService notificationService)
        {
            _deliveryDateModelFactory = deliveryDateModelFactory;
            _deliveryDateService = deliveryDateService;
            _commandService = commandService;
            _localeStringResourceService = localeStringResourceService;
            _activityLogService = activityLogService;
            _notificationService = notificationService;
        }
        #endregion

        #region Fields
        private readonly IDeliveryDateModelFactory _deliveryDateModelFactory;
        private readonly IDeliveryDateService _deliveryDateService;
        private readonly ICommandService _commandService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IActivityLogService _activityLogService;
        private readonly INotificationService _notificationService;
        #endregion

        #region Methods
        [CheckingPermissions(permissions: CommandNames.ManageDeliveryDate)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageDeliveryDate)]
        public IActionResult List()
            => View(new DeliveryDateSearchModel());

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageDeliveryDate)]
        public IActionResult List(DeliveryDateSearchModel searchModel)
        {
            var model = _deliveryDateModelFactory.PrepareDeliveryDateListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageDeliveryDate)]
        public IActionResult Create()
        {
            var model = _deliveryDateModelFactory.PrepareDeliveryDateModel(new DeliveryDateModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageDeliveryDate)]
        public IActionResult Create(DeliveryDateModel model)
        {
            if (ModelState.IsValid)
            {
                var deliveryDate = model.ToEntity<DeliveryDate, Guid>();

                //add delivery date
                _deliveryDateService.Add(deliveryDate);

                //add activity log for create delivery date
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.DeliveryDate.Create"), deliveryDate);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = deliveryDate.Id });
            }

            model = _deliveryDateModelFactory.PrepareDeliveryDateModel(model, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageDeliveryDate)]
        public IActionResult Edit(Guid Id)
        {
            if(Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var deliveryDate = _deliveryDateService.GetById(Id);
            if(deliveryDate == null)
            {
                return RedirectToAction("List");
            }

            if(deliveryDate.RemoverId.HasValue && deliveryDate.RemoverId.Value != Guid.Empty)
            {
                _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Notification.Error.EntityRemove"));
                return RedirectToAction("List");
            }

            var model = _deliveryDateModelFactory.PrepareDeliveryDateModel(null, deliveryDate);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageDeliveryDate)]
        public IActionResult Edit(DeliveryDateModel model)
        {
            if (ModelState.IsValid)
            {
                var deliveryDate = model.ToEntity<DeliveryDate, Guid>();

                //add delivery date
                _deliveryDateService.Edit(deliveryDate);

                //add activity log for edit delivery date
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.DeliveryDate.Edit"), deliveryDate);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = deliveryDate.Id });
            }

            model = _deliveryDateModelFactory.PrepareDeliveryDateModel(model, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageDeliveryDate)]
        public IActionResult Delete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var deliveryDate = _deliveryDateService.GetById(Id);
            if (deliveryDate == null)
            {
                return RedirectToAction("List");
            }

            _deliveryDateService.Delete(deliveryDate);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));

            return RedirectToAction("List");
        }
        #endregion
    }
}
