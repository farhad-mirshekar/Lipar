using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Services.Notification;
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
    public class MessageTemplateController : BaseAdminController
    {
        #region Ctor
        public MessageTemplateController(IMessageTemplateModelFactory messageTemplateModelFactory
                                    , ILocaleStringResourceService localeStringResourceService
                                    , INotificationService notificationService
                                    , IMessageTemplateService messageTemplateService)
        {
            _messageTemplateModelFactory = messageTemplateModelFactory;
            _localeStringResourceService = localeStringResourceService;
            _notificationService = notificationService;
            _messageTemplateService = messageTemplateService;
        }
        #endregion

        #region Fields
        private readonly IMessageTemplateModelFactory _messageTemplateModelFactory;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly INotificationService _notificationService;
        private readonly IMessageTemplateService _messageTemplateService;
        #endregion

        #region Methods

        [CheckingPermissions(permissions: CommandNames.ManageMessageTemplate)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageMessageTemplate)]
        public IActionResult List()
        {
            var searchModel = new MessageTemplateSearchModel();

            return View(searchModel);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMessageTemplate)]
        public IActionResult List(MessageTemplateSearchModel searchModel)
        {
            var model = _messageTemplateModelFactory.PrepareMessageTemplateListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageMessageTemplate)]
        public IActionResult Create()
        {
            var model = _messageTemplateModelFactory.PrepareMessageTemplateModel(new MessageTemplateModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMessageTemplate)]
        public IActionResult Create(MessageTemplateModel model)
        {
            if (ModelState.IsValid)
            {
                var messageTemplate = model.ToEntity<MessageTemplate, Guid>();

                //add message template
                _messageTemplateService.Add(messageTemplate);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = messageTemplate.Id });
            }

            model = _messageTemplateModelFactory.PrepareMessageTemplateModel(model, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageMessageTemplate)]
        public IActionResult Edit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var messageTemplate = _messageTemplateService.GetById(Id);
            if (messageTemplate == null)
            {
                return RedirectToAction("List");
            }

            var model = _messageTemplateModelFactory.PrepareMessageTemplateModel(null, messageTemplate);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMessageTemplate)]
        public IActionResult Edit(MessageTemplateModel model)
        {
            if (ModelState.IsValid)
            {
                var messageTemplate = model.ToEntity<MessageTemplate,Guid>();

                //edit message template
                _messageTemplateService.Edit(messageTemplate);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = messageTemplate.Id });
            }

            model = _messageTemplateModelFactory.PrepareMessageTemplateModel(model, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageMessageTemplate)]
        public IActionResult Delete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var messageTemplate = _messageTemplateService.GetById(Id);
            if (messageTemplate == null)
            {
                return RedirectToAction("List");
            }

            _messageTemplateService.Delete(messageTemplate);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));

            return RedirectToAction("List");
        }
        #endregion
    }
}
