using Lipar.Core.Caching;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Portal;
using Lipar.Web.Areas.Admin.Infrastructure.Cache;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class CategoryPortalController : BaseAdminController
    {
        #region Fields
        private readonly ICategoryModelFactory _categoryModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ICommandService _commandService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Ctor
        public CategoryPortalController(ICategoryModelFactory categoryModelFactory
                                      , ICategoryService categoryService
                                      , IStaticCacheManager cacheManager
                                      , ICommandService commandService
                                      , IActivityLogService activityLogService
                                      , ILocaleStringResourceService localeStringResourceService)
        {
            _categoryModelFactory = categoryModelFactory;
            _categoryService = categoryService;
            _cacheManager = cacheManager;
            _commandService = commandService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
        => View(new CategorySearchModel());

        [HttpPost]
        public IActionResult List(CategorySearchModel searchModel)
        {
            if (!_commandService.CheckPermission("ManageCategoryPortal"))
                return AccessDeniedView();

            var categories = _categoryModelFactory.PrepareCategoryListModel(searchModel);

            return Json(categories);
        }

        public IActionResult Create()
        {
            if (!_commandService.CheckPermission("ManageCategoryPortal"))
                return AccessDeniedView();

            var model = _categoryModelFactory.PrepareCategoryModel(new CategoryModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            if (!_commandService.CheckPermission("ManageCategoryPortal"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var category = model.ToEntity<Category>();

                if (category.ParentId == 0)
                    category.ParentId = null;

                _categoryService.Add(category);

                // add activity log for create category
                _activityLogService.Add("Admin.CategoryPortal.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.CategoryPortal.Create"), category);

                //remove cache for dropdown
                _cacheManager.Remove(LiparModelCacheDefaults.Category_Portal_List_Key);

                return RedirectToAction("List"); //success add
            }

            model = _categoryModelFactory.PrepareCategoryModel(model, null);
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            if (!_commandService.CheckPermission("ManageCategoryPortal"))
                return AccessDeniedView();

            var category = _categoryService.GetById(Id);
            if (category == null)
                return RedirectToAction("List");

            var model = _categoryModelFactory.PrepareCategoryModel(null, category);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CategoryModel model)
        {
            if (!_commandService.CheckPermission("ManageCategoryPortal"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var category = model.ToEntity<Category>();

                _categoryService.Edit(category);

                // add activity log for edit category
                _activityLogService.Add("Admin.CategoryPortal.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.CategoryPortal.Edit"), category);

                //remove cache for dropdown
                _cacheManager.Remove(LiparModelCacheDefaults.Category_Portal_List_Key);

                return RedirectToAction("List"); //success add
            }

            model = _categoryModelFactory.PrepareCategoryModel(model, null);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (!_commandService.CheckPermission("ManageCategoryPortal"))
                return AccessDeniedView();

            var category = _categoryService.GetById(Id);

            if (category == null)
                return RedirectToAction("List");

            _categoryService.Delete(category);

            // add activity log for delete category
            _activityLogService.Add("Admin.CategoryPortal.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.CategoryPortal.Delete"), category);

            return RedirectToAction("List");
        }
        #endregion
    }
}
