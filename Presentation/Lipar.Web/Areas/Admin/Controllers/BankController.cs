using Lipar.Core;
using Lipar.Entities.Domain.Financial;
using Lipar.Services.Financial.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Factories.Financial;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Financial;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC;
using Microsoft.AspNetCore.Mvc;

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
                            , ICommandService commandService
                            , IActivityLogService activityLogService
                            , ILocaleStringResourceService localeStringResourceService
                            , INotificationService notificationService
                            , IWorkContext workContext
                            , IBankPortService bankPortService)
        {
            _bankService = bankService;
            _bankModelFactory = bankModelFactory;
            _commandService = commandService;
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
        private readonly ICommandService _commandService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly INotificationService _notificationService;
        private readonly IWorkContext _workContext;
        private readonly IBankPortService _bankPortService;
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new BankSearchModel());

        [HttpPost]
        public IActionResult List(BankSearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _bankModelFactory.PrepareBankListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _bankModelFactory.PrepareBankModel(new BankModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(BankModel model)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var bank = model.ToEntity<Bank>();

                bank.UserId = _workContext.CurrentUser.Id;
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

        public IActionResult Edit(int Id)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
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
        public IActionResult Edit(BankModel model)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var bank = model.ToEntity<Bank>();

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
        public IActionResult Delete(int Id)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
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
        public IActionResult BankPortList(BankPortSearchModel searchModel)
        {
            var model = _bankModelFactory.PrepareBankPortListModel(searchModel);

            return Json(model);
        }

        public IActionResult BankPortCreate(int bankId)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (bankId == 0)
            {
                return RedirectToAction("List");
            }

            var model = _bankModelFactory.PrepareBankPortModel(new BankPortModel(), null);
            model.BankId = bankId;

            return View(model);
        }

        [HttpPost]
        public IActionResult BankPortCreate(BankPortModel model)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var bankPort = model.ToEntity<BankPort>();

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

        public IActionResult BankPortEdit(int id)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (id == 0)
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
        public IActionResult BankPortEdit(BankPortModel model)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var bankPort = model.ToEntity<BankPort>();

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
        public IActionResult BankPortDelete(int id)
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (id == 0)
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
