using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class ContactUsController : BaseAdminController
    {
        #region Fields
        private readonly IContactUsService _contactUsService;
        private readonly IContactUsModelFactory _contactUsModelFactory;
        private readonly ICommandService _commandService;
        private readonly INotificationService _notificationService;
        #endregion

        #region Ctor
        public ContactUsController(IContactUsService contactUsService
                                 , IContactUsModelFactory contactUsModelFactory
                                 , ICommandService commandService
                                 , INotificationService notificationService)
        {
            _contactUsService = contactUsService;
            _contactUsModelFactory = contactUsModelFactory;
            _commandService = commandService;
            _notificationService = notificationService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
        {
            var permission = _commandService.CheckPermission("ManageContactUs");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _contactUsModelFactory.PrepareContactUsSearchModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult List(ContactUsSearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageContactUs");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _contactUsModelFactory.PrepareContactUsListModel(searchModel);
            return Json(model);
        }

        public IActionResult View(int Id)
        {
            var permission = _commandService.CheckPermission("ManageContactUs");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if(Id == 0)
            {
                return RedirectToAction("List");
            }

            var contactUs = _contactUsService.GetById(Id);
            if(contactUs == null)
            {
                return RedirectToAction("List");
            }

            var model = _contactUsModelFactory.PrepareContactUsModel(null, contactUs);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var permission = _commandService.CheckPermission("ManageContactUs");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
            {
                return RedirectToAction("List");
            }

            var contactUs = _contactUsService.GetById(Id);
            if (contactUs == null)
            {
                return RedirectToAction("List");
            }

            _contactUsService.Delete(contactUs);

            _notificationService.SusscessNotification("حذف با موفقیت انجام شد");

            return RedirectToAction("List");
        }
        #endregion
    }
}
