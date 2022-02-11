using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Portal;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Lipar.Web.Framework.MVC.Filters;
using Lipar.Web.Areas.Admin.Helpers;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class StaticPageController : BaseAdminController
    {
        #region Ctor
        public StaticPageController(IStaticPageModelFactory staticPageModelFactory
                                  , IStaticPageService staticPageService
                                  , IUrlRecordService urlRecordService
                                  , IActivityLogService activityLogService
                                  , ILocaleStringResourceService localeStringResourceService)
        {
            _staticPageModelFactory = staticPageModelFactory;
            _staticPageService = staticPageService;
            _urlRecordService = urlRecordService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Fields
        private readonly IStaticPageModelFactory _staticPageModelFactory;
        private readonly IStaticPageService _staticPageService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Methods

        [CheckingPermissions(permissions: CommandNames.ManageStaticPage)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageStaticPage)]
        public IActionResult List()
            => View(new StaticPageSearchModel());

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageStaticPage)]
        public IActionResult List(StaticPageSearchModel searchModel)
        {
            var model = _staticPageModelFactory.PrepareStaticPageListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageStaticPage)]
        public IActionResult Create()
        {
            var model = _staticPageModelFactory.PrepareStaticPageModel(new StaticPageModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageStaticPage)]
        public IActionResult Create(StaticPageModel model)
        {
            if (ModelState.IsValid)
            {
                var staticPage = model.ToEntity<StaticPage, Guid>();

                //insert static page
                _staticPageService.Add(staticPage);

                //insert activity log for create static page
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.StaticPage.Create"), staticPage);

                //insert slug for url record : seo page
                _urlRecordService.SaveSlug<StaticPage,Guid>(staticPage, staticPage.Title, 1);

                //return success 
                return RedirectToAction("Edit", new { ID = staticPage.Id });
            }

            model = _staticPageModelFactory.PrepareStaticPageModel(model, null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageStaticPage)]
        public IActionResult Edit(Guid Id)
        {
            if (Id == Guid.Empty)
                return RedirectToAction("List");

            var staticPage = _staticPageService.GetById(Id);
            if (staticPage == null)
                return RedirectToAction("List");

            var model = _staticPageModelFactory.PrepareStaticPageModel(null, staticPage);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageStaticPage)]
        public IActionResult Edit(StaticPageModel model)
        {
            if (ModelState.IsValid)
            {
                var staticPage = model.ToEntity<StaticPage, Guid>();

                //edit static page
                _staticPageService.Edit(staticPage);

                //insert activity log for edit static page
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.StaticPage.Edit"), staticPage);

                //insert slug for url record : seo page

                _urlRecordService.SaveSlug<StaticPage,Guid>(staticPage, staticPage.Title, 1);

                //return success
                return RedirectToAction("Edit", new { ID = staticPage.Id });
            }

            model = _staticPageModelFactory.PrepareStaticPageModel(model, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageStaticPage)]
        public IActionResult Delete(Guid Id)
        {
            if (Id == Guid.Empty)
                return RedirectToAction("List");

            var staticPage = _staticPageService.GetById(Id);
            if (staticPage == null)
                return RedirectToAction("List");

            //delete static page
            _staticPageService.Delete(staticPage);

            //insert activity log for create static page
            _activityLogService.Add("Admin.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.StaticPage.Delete"), staticPage);

            return RedirectToAction("List");
        }
        #endregion
    }
}
