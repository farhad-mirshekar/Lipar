using Lipar.Core.Caching;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Application;
using Lipar.Web.Areas.Admin.Infrastructure.Cache;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class CategoryProductController : BaseAdminController
    {
        #region Ctor
        public CategoryProductController(ICategoryModelFactory categoryModelFactory
                                       , ICategoryService categoryService
                                       , ICommandService commandService
                                       , ILocaleStringResourceService localeStringResourceService
                                       , IActivityLogService activityLogService
                                       , INotificationService notificationService
                                       , IStaticCacheManager cacheManager)
        {
            _categoryModelFactory = categoryModelFactory;
            _categoryService = categoryService;
            _commandService = commandService;
            _localeStringResourceService = localeStringResourceService;
            _activityLogService = activityLogService;
            _notificationService = notificationService;
            _cacheManager = cacheManager;
        }
        #endregion

        #region Fields
        private readonly ICategoryModelFactory _categoryModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly ICommandService _commandService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IActivityLogService _activityLogService;
        private readonly INotificationService _notificationService;
        private readonly IStaticCacheManager _cacheManager;
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new CategorySearchModel());

        [HttpPost]
        public IActionResult List(CategorySearchModel searchModel)
        {
            var permission = _commandService.CheckPermission("ManageCategoryProduct");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _categoryModelFactory.PrepareCategoryListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            var permission = _commandService.CheckPermission("ManageCategoryProduct");
            if (!permission)
            {
                return AccessDeniedView();
            }

            var model = _categoryModelFactory.PrepareCategoryModel(new CategoryModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            var permission = _commandService.CheckPermission("ManageCategoryProduct");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var category = model.ToEntity<Category>();

                if (category.ParentId == 0)
                {
                    category.ParentId = null;
                }

                //add category
                _categoryService.Add(category);

                //clear cache
                _cacheManager.Remove(LiparModelCacheDefaults.Category_Product_List_Key);

                //add activity log for create category
                _activityLogService.Add("Admin.CategoryProduct.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.CategoryProduct.Create"), category);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = category.Id });
            }

            model = _categoryModelFactory.PrepareCategoryModel(model,null);

            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            var permission = _commandService.CheckPermission("ManageCategoryProduct");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if(Id == 0)
            {
                return RedirectToAction("List");
            }

            var category = _categoryService.GetById(Id);
            if(category == null)
            {
                return RedirectToAction("List");
            }

            if (category.RemoverId.HasValue && category.RemoverId.Value != 0)
            {
                _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Notification.Error.EntityRemove"));
                return RedirectToAction("List");
            }

            var model = _categoryModelFactory.PrepareCategoryModel(null, category);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CategoryModel model)
        {
            var permission = _commandService.CheckPermission("ManageCategoryProduct");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var category = model.ToEntity<Category>();
                
                if (category.ParentId == 0)
                {
                    category.ParentId = null;
                }

                //edit category
                _categoryService.Edit(category);

                //clear cache
                _cacheManager.Remove(LiparModelCacheDefaults.Category_Product_List_Key);

                //add activity log for edit category
                _activityLogService.Add("Admin.CategoryProduct.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.CategoryProduct.Edit"), category);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = category.Id });
            }

            model = _categoryModelFactory.PrepareCategoryModel(model, null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var permission = _commandService.CheckPermission("ManageCategoryProduct");
            if (!permission)
            {
                return AccessDeniedView();
            }

            if (Id == 0)
            {
                return RedirectToAction("List");
            }

            var category = _categoryService.GetById(Id);
            if (category == null)
            {
                return RedirectToAction("List");
            }

            if (category.RemoverId.HasValue && category.RemoverId.Value != 0)
            {
                _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Notification.Error.EntityRemove"));
                return RedirectToAction("List");
            }

            //delete category
            _categoryService.Delete(category);

            //clear cache
            _cacheManager.Remove(LiparModelCacheDefaults.Category_Product_List_Key);

            //add activity log for delete category
            _activityLogService.Add("Admin.CategoryProduct.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.CategoryProduct.Delete"), category);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));

            return RedirectToAction("List");
        }
        #endregion
    }
}
