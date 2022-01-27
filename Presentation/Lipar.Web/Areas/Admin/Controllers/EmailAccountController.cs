using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class EmailAccountController : BaseAdminController
    {
        #region Ctor
        public EmailAccountController(IEmailAccountModelFactory emailAccountModelFactory
                                    , ICommandService commandService
                                    , ILocaleStringResourceService localeStringResourceService
                                    , INotificationService notificationService
                                    , IEmailAccountService emailAccountService)
        {
            _emailAccountModelFactory = emailAccountModelFactory;
            _commandService = commandService;
            _localeStringResourceService = localeStringResourceService;
            _notificationService = notificationService;
            _emailAccountService = emailAccountService;
        }
        #endregion

        #region Fields
        private readonly IEmailAccountModelFactory _emailAccountModelFactory;
        private readonly ICommandService _commandService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly INotificationService _notificationService;
        private readonly IEmailAccountService _emailAccountService;
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
        {
            var permission = _commandService.CheckPermission("ManageEmailAccount");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var searchModel = new EmailAccountSearchModel();
            
            return View(searchModel);
        }

        [HttpPost]
        public IActionResult List(EmailAccountSearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageEmailAccount");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _emailAccountModelFactory.PrepareEmailAccountListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            var permission = _commandService.CheckPermission("ManageBank");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _emailAccountModelFactory.PrepareEmailAccountModel(new EmailAccountModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EmailAccountModel model)
        {
            var permission = _commandService.CheckPermission("ManageEmailAccount");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var emailAccount = model.ToEntity<EmailAccount>();

                //add email account
                _emailAccountService.Add(emailAccount);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = emailAccount.Id });
            }

            model = _emailAccountModelFactory.PrepareEmailAccountModel(model, null);

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

            var emailAccount = _emailAccountService.GetById(Id);
            if (emailAccount == null)
            {
                return RedirectToAction("List");
            }

            var model = _emailAccountModelFactory.PrepareEmailAccountModel(null, emailAccount);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmailAccountModel model)
        {
            var permission = _commandService.CheckPermission("ManageEmailAccount");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var emailAccount = model.ToEntity<EmailAccount>();

                //edit email account
                _emailAccountService.Edit(emailAccount);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = emailAccount.Id });
            }

            model = _emailAccountModelFactory.PrepareEmailAccountModel(model, null);

            return View(model);
        }
        #endregion
    }
}
