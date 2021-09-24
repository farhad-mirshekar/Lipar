using Lipar.Entities.Domain.Portal;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Portal;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class StaticPageController : BaseAdminController
    {
        private readonly IStaticPageModelFactory _staticPageModelFactory;
        private readonly IStaticPageService _staticPageService;
        private readonly ICommandService _commandService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #region Fields

        #endregion

        #region Ctor
        public StaticPageController(IStaticPageModelFactory staticPageModelFactory
                                  , IStaticPageService staticPageService
                                  , ICommandService commandService
                                  , IUrlRecordService urlRecordService
                                  , IActivityLogService activityLogService
                                  , ILocaleStringResourceService localeStringResourceService)
        {
            _staticPageModelFactory = staticPageModelFactory;
            _staticPageService = staticPageService;
            _commandService = commandService;
            _urlRecordService = urlRecordService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new StaticPageSearchModel());

        [HttpPost]
        public IActionResult List(StaticPageSearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageStaticPage");
            if (!permission)
                return AccessDeniedView();

            var model = _staticPageModelFactory.PrepareStaticPageListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            var permission = _commandService.CheckPermission("ManageStaticPage");
            if (!permission)
                return AccessDeniedView();

            var model = _staticPageModelFactory.PrepareStaticPageModel(new StaticPageModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(StaticPageModel model)
        {
            var permission = _commandService.CheckPermission("ManageStaticPage");
            if (!permission)
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var staticPage = model.ToEntity<StaticPage>();

                //insert static page
                _staticPageService.Add(staticPage);

                //insert activity log for create static page
                _activityLogService.Add("Admin.StaticPage.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.StaticPage.Create"), staticPage);

                //insert slug for url record : seo page
                //language : 0
                _urlRecordService.SaveSlug<StaticPage>(staticPage, staticPage.Title, 0);

                //return success 
                return RedirectToAction("Edit", new { ID = staticPage.Id });
            }

            model = _staticPageModelFactory.PrepareStaticPageModel(model, null);

            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            var permission = _commandService.CheckPermission("ManageStaticPage");
            if (!permission)
                return AccessDeniedView();

            if (Id == 0)
                return RedirectToAction("List");

            var staticPage = _staticPageService.GetById(Id);
            if (staticPage == null)
                return RedirectToAction("List");

            var model = _staticPageModelFactory.PrepareStaticPageModel(null, staticPage);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(StaticPageModel model)
        {
            var permission = _commandService.CheckPermission("ManageStaticPage");
            if (!permission)
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var staticPage = model.ToEntity<StaticPage>();

                //edit static page
                _staticPageService.Edit(staticPage);

                //insert activity log for edit static page
                _activityLogService.Add("Admin.StaticPage.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.StaticPage.Edit"), staticPage);

                //insert slug for url record : seo page
                //language : 0
                _urlRecordService.SaveSlug<StaticPage>(staticPage, staticPage.Title, 0);

                //return success
                return RedirectToAction("Edit", new { ID = staticPage.Id });
            }

            model = _staticPageModelFactory.PrepareStaticPageModel(model, null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var permission = _commandService.CheckPermission("ManageStaticPage");
            if (!permission)
                return AccessDeniedView();


            if (Id == 0)
                return RedirectToAction("List");

            var staticPage = _staticPageService.GetById(Id);
            if (staticPage == null)
                return RedirectToAction("List");

            //delete static page
            _staticPageService.Delete(staticPage);

            //insert activity log for create static page
            _activityLogService.Add("Admin.StaticPage.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.StaticPage.Delete"), staticPage);

            return RedirectToAction("List");
        }
        #endregion
    }
}
