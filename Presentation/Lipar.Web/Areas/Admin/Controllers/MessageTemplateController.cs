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
    public class MessageTemplateController : BaseAdminController
    {
        #region Ctor
        public MessageTemplateController(IMessageTemplateModelFactory messageTemplateModelFactory
                                    , ICommandService commandService
                                    , ILocaleStringResourceService localeStringResourceService
                                    , INotificationService notificationService
                                    , IMessageTemplateService messageTemplateService)
        {
            _messageTemplateModelFactory = messageTemplateModelFactory;
            _commandService = commandService;
            _localeStringResourceService = localeStringResourceService;
            _notificationService = notificationService;
            _messageTemplateService = messageTemplateService;
        }
        #endregion

        #region Fields
        private readonly IMessageTemplateModelFactory _messageTemplateModelFactory;
        private readonly ICommandService _commandService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly INotificationService _notificationService;
        private readonly IMessageTemplateService _messageTemplateService;
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
        {
            var permission = _commandService.CheckPermission("ManageMessageTemplate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var searchModel = new MessageTemplateSearchModel();

            return View(searchModel);
        }

        [HttpPost]
        public IActionResult List(MessageTemplateSearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageMessageTemplate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _messageTemplateModelFactory.PrepareMessageTemplateListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            var permission = _commandService.CheckPermission("ManageMessageTemplate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _messageTemplateModelFactory.PrepareMessageTemplateModel(new MessageTemplateModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(MessageTemplateModel model)
        {
            var permission = _commandService.CheckPermission("ManageMessageTemplate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var messageTemplate = model.ToEntity<MessageTemplate>();

                //add message template
                _messageTemplateService.Add(messageTemplate);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = messageTemplate.Id });
            }

            model = _messageTemplateModelFactory.PrepareMessageTemplateModel(model, null);

            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            var permission = _commandService.CheckPermission("ManageMessageTemplate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
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
        public IActionResult Edit(MessageTemplateModel model)
        {
            var permission = _commandService.CheckPermission("ManageMessageTemplate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var messageTemplate = model.ToEntity<MessageTemplate>();

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
        public IActionResult Delete(int Id)
        {
            var permission = _commandService.CheckPermission("ManageMessageTemplate");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
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
