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
    public class DynamicPageController : BaseAdminController
    {
        #region Fields
        private readonly IDynamicPageModelFactory _dynamicPageModelFactory;
        private readonly IDynamicPageService _dynamicPageService;
        private readonly IDynamicPageDetailService _dynamicPageDetailService;
        private readonly ICommandService _commandService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IActivityLogService _activityLogService;
        private readonly IUrlRecordService _urlRecordService;
        #endregion

        #region Ctor
        public DynamicPageController(IDynamicPageModelFactory dynamicPageModelFactory
                                   , IDynamicPageService dynamicPageService
                                   , IDynamicPageDetailService dynamicPageDetailService
                                   , ICommandService commandService
                                   , ILocaleStringResourceService localeStringResourceService
                                   , IActivityLogService activityLogService
                                   , IUrlRecordService urlRecordService)
        {
            _dynamicPageModelFactory = dynamicPageModelFactory;
            _dynamicPageService = dynamicPageService;
            _dynamicPageDetailService = dynamicPageDetailService;
            _commandService = commandService;
            _localeStringResourceService = localeStringResourceService;
            _activityLogService = activityLogService;
            _urlRecordService = urlRecordService;
        }
        #endregion

        #region Dynamic Page Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new DynamicPageSearchModel());

        [HttpPost]
        public IActionResult List(DynamicPageSearchModel searchModel)
        {
            var model = _dynamicPageModelFactory.PrepareDynamicPageListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if(!permission)
            {
                return AccessDeniedView();
            }

            var model = _dynamicPageModelFactory.PrepareDynamicPageModel(new DynamicPageModel(), null);
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(DynamicPageModel model)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var dynamicPage = model.ToEntity<DynamicPage>();

                _dynamicPageService.Add(dynamicPage);

                //insert activity log for create dynamic page
                _activityLogService.Add("Admin.DynamicPage.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.DynamicPage.Create"), dynamicPage);

                return RedirectToAction("Edit", new { Id = dynamicPage.Id });
            }

            model = _dynamicPageModelFactory.PrepareDynamicPageModel(model, null);

            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
                return RedirectToAction("List");

            var dynamicPage = _dynamicPageService.GetById(Id);
            if(dynamicPage == null)
            {
                return RedirectToAction("List");
            }

            var model = _dynamicPageModelFactory.PrepareDynamicPageModel(null, dynamicPage);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DynamicPageModel model)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var dynamicPage = model.ToEntity<DynamicPage>();

                _dynamicPageService.Edit(dynamicPage);

                //insert activity log for edit dynamic page
                _activityLogService.Add("Admin.DynamicPage.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.DynamicPage.Edit"), dynamicPage);

                return RedirectToAction("Edit", new { Id = dynamicPage.Id });
            }

            model = _dynamicPageModelFactory.PrepareDynamicPageModel(model, null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
                return RedirectToAction("List");

            var dynamicPage = _dynamicPageService.GetById(Id);
            if (dynamicPage == null)
            {
                return RedirectToAction("List");
            }

            _dynamicPageService.Delete(dynamicPage);

            //insert activity log for delete dynamic page
            _activityLogService.Add("Admin.DynamicPage.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.DynamicPage.Delete"), dynamicPage);

            return RedirectToAction("List");
        }
        #endregion

        #region Dynamic Page Detail Methods
        public IActionResult DynamicPageDetails(int? filterByPageId)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var dynamicPage = _dynamicPageService.GetById(filterByPageId ?? 0);
            if (dynamicPage == null && filterByPageId.HasValue)
                return RedirectToAction("DynamicPageDetails");

            var model = _dynamicPageModelFactory.PrepareDynamicPageDetailSearchModel(new DynamicPageDetailSearchModel(), dynamicPage);
            return View(model);
        }

        [HttpPost]
        public IActionResult DynamicPageDetails(DynamicPageDetailSearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _dynamicPageModelFactory.PrepareDynamicPageDetailListModel(searchModel);
            return Json(model);
        }

        public IActionResult DynamicPageDetailCreate(int pageId)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (pageId == 0)
                return RedirectToAction("List");

            var dynamicPage = _dynamicPageService.GetById(pageId);
            if(dynamicPage == null)
                return RedirectToAction("List");

            if (dynamicPage.RemoverId.HasValue)
                return RedirectToAction("List");

            var dynamicPageDetailModel = new DynamicPageDetailModel
            {
                DynamicPageId = dynamicPage.Id
            };

            var model = _dynamicPageModelFactory.PrepareDynamicPageDetailModel(dynamicPageDetailModel, null);
            return View(model);
        }

        [HttpPost]
        public IActionResult DynamicPageDetailCreate(DynamicPageDetailModel model)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var dynamicPageDetail = model.ToEntity<DynamicPageDetail>();

                _dynamicPageDetailService.Add(dynamicPageDetail);

                //insert activity log for create dynamic page
                _activityLogService.Add("Admin.DynamicPageDetail.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.DynamicPageDetail.Create"), dynamicPageDetail);

                _urlRecordService.SaveSlug<DynamicPageDetail>(dynamicPageDetail, dynamicPageDetail.Title, 0);

                return RedirectToAction("DynamicPageDetailEdit", new { Id = dynamicPageDetail.Id });
            }

            model = _dynamicPageModelFactory.PrepareDynamicPageDetailModel(model, null);

            return View(model);
        }

        public IActionResult DynamicPageDetailEdit(int Id)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var dynamicPageDetail = _dynamicPageDetailService.GetById(Id);
            if (dynamicPageDetail == null || (dynamicPageDetail.RemoverId.HasValue))
                return RedirectToAction("List");

            var model = _dynamicPageModelFactory.PrepareDynamicPageDetailModel(null,dynamicPageDetail);
            return View(model);
        }

        [HttpPost]
        public IActionResult DynamicPageDetailEdit(DynamicPageDetailModel model)
        {
            var permission = _commandService.CheckPermission("ManageDynamicPage");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var dynamicPageDetail = model.ToEntity<DynamicPageDetail>();

                _dynamicPageDetailService.Edit(dynamicPageDetail);

                //insert activity log for create dynamic page
                _activityLogService.Add("Admin.DynamicPageDetail.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.DynamicPageDetail.Edit"), dynamicPageDetail);

                _urlRecordService.SaveSlug<DynamicPageDetail>(dynamicPageDetail, dynamicPageDetail.Title, 0);

                return RedirectToAction("DynamicPageDetailEdit", new { Id = dynamicPageDetail.Id });
            }

            model = _dynamicPageModelFactory.PrepareDynamicPageDetailModel(model, null);

            return View(model);
        }
        #endregion
    }
}
