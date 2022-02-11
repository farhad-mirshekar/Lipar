using Lipar.Core;
using Lipar.Entities.Domain.Financial;
using Lipar.Services.Financial.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Factories.Financial;
using Lipar.Web.Areas.Admin.Helpers;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Financial;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC;
using Lipar.Web.Framework.MVC.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// bank controller
    /// </summary>
    public class BankController : BaseAdminController
    {
        #region Ctor
        public BankController(IBankService bankService
                            , IBankModelFactory bankModelFactory
                            , IActivityLogService activityLogService
                            , ILocaleStringResourceService localeStringResourceService
                            , INotificationService notificationService
                            , IWorkContext workContext
                            , IBankPortService bankPortService)
        {
            _bankService = bankService;
            _bankModelFactory = bankModelFactory;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _notificationService = notificationService;
            _workContext = workContext;
            _bankPortService = bankPortService;
        }
        #endregion

        #region Fields
        private readonly IBankService _bankService;
        private readonly IBankModelFactory _bankModelFactory;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly INotificationService _notificationService;
        private readonly IWorkContext _workContext;
        private readonly IBankPortService _bankPortService;
        #endregion

        #region Methods

        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult List()
            => View(new BankSearchModel());

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult List(BankSearchModel searchModel)
        {
            var model = _bankModelFactory.PrepareBankListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult Create()
        {
            var model = _bankModelFactory.PrepareBankModel(new BankModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult Create(BankModel model)
        {
            if (ModelState.IsValid)
            {
                var bank = model.ToEntity<Bank,Guid>();

                //add bank
                _bankService.Add(bank);

                //add activity log for create bank
                //_activityLogService.Add("Admin.Bank.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.Bank.Create"), bank);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = bank.Id });
            }

            model = _bankModelFactory.PrepareBankModel(model, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult Edit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var bank = _bankService.GetById(Id);
            if (bank == null)
            {
                return RedirectToAction("List");
            }

            var model = _bankModelFactory.PrepareBankModel(null, bank);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult Edit(BankModel model)
        {
            if (ModelState.IsValid)
            {
                var bank = model.ToEntity<Bank,Guid>();

                //edit bank
                _bankService.Edit(bank);

                //add activity log for edit bank
                _activityLogService.Add("Admin.Bank.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Bank.Edit"), bank);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = bank.Id });
            }

            model = _bankModelFactory.PrepareBankModel(model, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult Delete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var bank = _bankService.GetById(Id);
            if (bank == null)
            {
                return RedirectToAction("List");
            }

            _bankService.Delete(bank);

            //add activity log for delete bank
            _activityLogService.Add("Admin.Bank.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.Bank.Delete"), bank);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));

            return RedirectToAction("List");
        }
        #endregion

        #region Bank Port Methods

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult BankPortList(BankPortSearchModel searchModel)
        {
            var model = _bankModelFactory.PrepareBankPortListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult BankPortCreate(Guid bankId)
        {
            if (bankId == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var model = _bankModelFactory.PrepareBankPortModel(new BankPortModel(), null);
            model.BankId = bankId;

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult BankPortCreate(BankPortModel model)
        {
            if (ModelState.IsValid)
            {
                var bankPort = model.ToEntity<BankPort,Guid>();

                //add bank port
                _bankPortService.Add(bankPort);

                //add activity log for create bank port
                //_activityLogService.Add("Admin.BankPort.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.BankPort.Create"), bank);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { id = bankPort.BankId });
            }

            model = _bankModelFactory.PrepareBankPortModel(model, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult BankPortEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var bankPort = _bankPortService.GetById(id);
            if (bankPort == null)
            {
                return RedirectToAction("List");
            }

            var model = _bankModelFactory.PrepareBankPortModel(null, bankPort);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult BankPortEdit(BankPortModel model)
        {
            if (ModelState.IsValid)
            {
                var bankPort = model.ToEntity<BankPort,Guid>();

                //edit bank port
                _bankPortService.Edit(bankPort);

                //add activity log for edit bank port
                //_activityLogService.Add("Admin.BankPort.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.BankPort.Edit"), bank);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { id = bankPort.BankId });
            }

            model = _bankModelFactory.PrepareBankPortModel(model, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageBank)]
        public IActionResult BankPortDelete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new NullJsonResult();
            }

            var bankPort = _bankPortService.GetById(id);
            if (bankPort == null)
            {
                return new NullJsonResult();
            }

            _bankPortService.Delete(bankPort);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));


            return new NullJsonResult();
        }
        #endregion
    }
}
