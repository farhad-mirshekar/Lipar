using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Helpers;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class EmailAccountController : BaseAdminController
    {
        #region Ctor
        public EmailAccountController(IEmailAccountModelFactory emailAccountModelFactory
                                    , ILocaleStringResourceService localeStringResourceService
                                    , INotificationService notificationService
                                    , IEmailAccountService emailAccountService)
        {
            _emailAccountModelFactory = emailAccountModelFactory;
            _localeStringResourceService = localeStringResourceService;
            _notificationService = notificationService;
            _emailAccountService = emailAccountService;
        }
        #endregion

        #region Fields
        private readonly IEmailAccountModelFactory _emailAccountModelFactory;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly INotificationService _notificationService;
        private readonly IEmailAccountService _emailAccountService;
        #endregion

        #region Methods

        [CheckingPermissions(permissions: CommandNames.ManageEmailAccount)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageEmailAccount)]
        public IActionResult List()
        {
            var searchModel = new EmailAccountSearchModel();
            
            return View(searchModel);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageEmailAccount)]
        public IActionResult List(EmailAccountSearchModel searchModel)
        {
            var model = _emailAccountModelFactory.PrepareEmailAccountListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageEmailAccount)]
        public IActionResult Create()
        {
            var model = _emailAccountModelFactory.PrepareEmailAccountModel(new EmailAccountModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageEmailAccount)]
        public IActionResult Create(EmailAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var emailAccount = model.ToEntity<EmailAccount, Guid>();

                //add email account
                _emailAccountService.Add(emailAccount);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = emailAccount.Id });
            }

            model = _emailAccountModelFactory.PrepareEmailAccountModel(model, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageEmailAccount)]
        public IActionResult Edit(Guid Id)
        {
            if (Id == Guid.Empty)
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
        [CheckingPermissions(permissions: CommandNames.ManageEmailAccount)]
        public IActionResult Edit(EmailAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var emailAccount = model.ToEntity<EmailAccount, Guid>();

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
