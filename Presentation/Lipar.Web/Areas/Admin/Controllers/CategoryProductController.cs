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
using System;
using Lipar.Web.Framework.MVC.Filters;
using Lipar.Web.Areas.Admin.Helpers;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class CategoryProductController : BaseAdminController
    {
        #region Ctor
        public CategoryProductController(ICategoryModelFactory categoryModelFactory
                                       , ICategoryService categoryService
                                       , ILocaleStringResourceService localeStringResourceService
                                       , IActivityLogService activityLogService
                                       , INotificationService notificationService
                                       , IStaticCacheManager cacheManager)
        {
            _categoryModelFactory = categoryModelFactory;
            _categoryService = categoryService;
            _localeStringResourceService = localeStringResourceService;
            _activityLogService = activityLogService;
            _notificationService = notificationService;
            _cacheManager = cacheManager;
        }
        #endregion

        #region Fields
        private readonly ICategoryModelFactory _categoryModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IActivityLogService _activityLogService;
        private readonly INotificationService _notificationService;
        private readonly IStaticCacheManager _cacheManager;
        #endregion

        #region Methods
        [CheckingPermissions(permissions: CommandNames.ManageCategoryProduct)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageCategoryProduct)]
        public IActionResult List()
            => View(new CategorySearchModel());

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageCategoryProduct)]
        public IActionResult List(CategorySearchModel searchModel)
        {
            var model = _categoryModelFactory.PrepareCategoryListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageCategoryProduct)]
        public IActionResult Create()
        {
            var model = _categoryModelFactory.PrepareCategoryModel(new CategoryModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageCategoryProduct)]
        public IActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = model.ToEntity<Category, Guid>();

                //add category
                _categoryService.Add(category);

                //clear cache
                _cacheManager.Remove(LiparModelCacheDefaults.Category_Product_List_Key);

                //add activity log for create category
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.CategoryProduct.Create"), category);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityCreate"));

                return RedirectToAction("Edit", new { Id = category.Id });
            }

            model = _categoryModelFactory.PrepareCategoryModel(model,null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageCategoryProduct)]
        public IActionResult Edit(Guid Id)
        {
            if(Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var category = _categoryService.GetById(Id);
            if(category == null)
            {
                return RedirectToAction("List");
            }

            if (category.RemoverId.HasValue && category.RemoverId.Value != Guid.Empty)
            {
                _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Notification.Error.EntityRemove"));
                return RedirectToAction("List");
            }

            var model = _categoryModelFactory.PrepareCategoryModel(null, category);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageCategoryProduct)]
        public IActionResult Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = model.ToEntity<Category, Guid>();

                //edit category
                _categoryService.Edit(category);

                //clear cache
                _cacheManager.Remove(LiparModelCacheDefaults.Category_Product_List_Key);

                //add activity log for edit category
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.CategoryProduct.Edit"), category);

                //notification
                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityEdit"));

                return RedirectToAction("Edit", new { Id = category.Id });
            }

            model = _categoryModelFactory.PrepareCategoryModel(model, null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageCategoryProduct)]
        public IActionResult Delete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var category = _categoryService.GetById(Id);
            if (category == null)
            {
                return RedirectToAction("List");
            }

            if (category.RemoverId.HasValue && category.RemoverId.Value != Guid.Empty)
            {
                _notificationService.ErrorNotification(_localeStringResourceService.GetResource("Admin.Notification.Error.EntityRemove"));
                return RedirectToAction("List");
            }

            //delete category
            _categoryService.Delete(category);

            //clear cache
            _cacheManager.Remove(LiparModelCacheDefaults.Category_Product_List_Key);

            //add activity log for delete category
            _activityLogService.Add("Admin.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.CategoryProduct.Delete"), category);

            //notification
            _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Admin.Notification.Success.EntityRemove"));

            return RedirectToAction("List");
        }
        #endregion
    }
}
