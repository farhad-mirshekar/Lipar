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
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new DeliveryDateSearchModel());

        [HttpPost]
        public IActionResult List(DeliveryDateSearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageDeliveryDate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _deliveryDateModelFactory.PrepareDeliveryDateListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            var permission = _commandService.CheckPermission("ManageDeliveryDate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _deliveryDateModelFactory.PrepareDeliveryDateModel(new DeliveryDateModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(DeliveryDateModel model)
        {
            var permission = _commandService.CheckPermission("ManageDeliveryDate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var deliveryDate = model.ToEntity<DeliveryDate>();

                //add delivery date
                _deliveryDateService.Add(deliveryDate);

                //add activity log for create delivery date
                _activityLogService.Add("Admin.DeliveryDate.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.DeliveryDate.Create"), deliveryDate);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = deliveryDate.Id });
            }

            model = _deliveryDateModelFactory.PrepareDeliveryDateModel(model, null);

            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            var permission = _commandService.CheckPermission("ManageDeliveryDate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if(Id == 0)
            {
                return RedirectToAction("List");
            }

            var deliveryDate = _deliveryDateService.GetById(Id);
            if(deliveryDate == null)
            {
                return RedirectToAction("List");
            }

            if(deliveryDate.RemoverId.HasValue && deliveryDate.RemoverId.Value != 0)
            {
                _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Notification.Error.EntityRemove"));
                return RedirectToAction("List");
            }

            var model = _deliveryDateModelFactory.PrepareDeliveryDateModel(null, deliveryDate);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DeliveryDateModel model)
        {
            var permission = _commandService.CheckPermission("ManageDeliveryDate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var deliveryDate = model.ToEntity<DeliveryDate>();

                //add delivery date
                _deliveryDateService.Edit(deliveryDate);

                //add activity log for edit delivery date
                _activityLogService.Add("Admin.DeliveryDate.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.DeliveryDate.Edit"), deliveryDate);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = deliveryDate.Id });
            }

            model = _deliveryDateModelFactory.PrepareDeliveryDateModel(model, null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var permission = _commandService.CheckPermission("ManageDeliveryDate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
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
