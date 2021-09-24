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
    public class ShippingCostController : BaseAdminController
    {
        #region Ctor
        public ShippingCostController(IShippingCostModelFactory shippingCostModelFactory
                                    , IShippingCostService shippingCostService
                                    , ICommandService commandService
                                    , ILocaleStringResourceService localeStringResourceService
                                    , IActivityLogService activityLogService
                                    , INotificationService notificationService)
        {
            _shippingCostModelFactory = shippingCostModelFactory;
            _shippingCostService = shippingCostService;
            _commandService = commandService;
            _localeStringResourceService = localeStringResourceService;
            _activityLogService = activityLogService;
            _notificationService = notificationService;
        }
        #endregion

        #region Fields
        private readonly IShippingCostModelFactory _shippingCostModelFactory;
        private readonly IShippingCostService _shippingCostService;
        private readonly ICommandService _commandService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IActivityLogService _activityLogService;
        private readonly INotificationService _notificationService;
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new ShippingCostSearchModel());

        [HttpPost]
        public IActionResult List(ShippingCostSearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageShippingCost");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _shippingCostModelFactory.PrepareShippingCostListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            var permission = _commandService.CheckPermission("ManageShippingCost");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _shippingCostModelFactory.PrepareShippingCostModel(new ShippingCostModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ShippingCostModel model)
        {
            var permission = _commandService.CheckPermission("ManageShippingCost");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var shippingCost = model.ToEntity<ShippingCost>();

                //add shipping cost
                _shippingCostService.Add(shippingCost);

                //add activity log for create shipping cost
                _activityLogService.Add("Admin.ShippingCost.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.ShippingCost.Create"), shippingCost);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = shippingCost.Id });
            }

            model = _shippingCostModelFactory.PrepareShippingCostModel(model, null);

            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            var permission = _commandService.CheckPermission("ManageShippingCost");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
            {
                return RedirectToAction("List");
            }

            var shippingCost = _shippingCostService.GetById(Id);
            if (shippingCost == null)
            {
                return RedirectToAction("List");
            }

            if (shippingCost.RemoverId.HasValue && shippingCost.RemoverId.Value != 0)
            {
                _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Notification.Error.EntityRemove"));
                return RedirectToAction("List");
            }

            var model = _shippingCostModelFactory.PrepareShippingCostModel(null, shippingCost);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ShippingCostModel model)
        {
            var permission = _commandService.CheckPermission("ManageShippingCost");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var shippingCost = model.ToEntity<ShippingCost>();

                //edit shipping cost
                _shippingCostService.Edit(shippingCost);

                //add activity log for edit shipping cost
                _activityLogService.Add("Admin.ShippingCost.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.ShippingCost.Edit"), shippingCost);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = shippingCost.Id });
            }

            model = _shippingCostModelFactory.PrepareShippingCostModel(model, null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var permission = _commandService.CheckPermission("ManageShippingCost");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
            {
                return RedirectToAction("List");
            }

            var shippingCost = _shippingCostService.GetById(Id);
            if (shippingCost == null)
            {
                return RedirectToAction("List");
            }

            _shippingCostService.Delete(shippingCost);

            //add activity log for delete shipping cost
            _activityLogService.Add("Admin.ShippingCost.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.ShippingCost.Delete"), shippingCost);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));

            return RedirectToAction("List");
        }
        #endregion
    }
}
