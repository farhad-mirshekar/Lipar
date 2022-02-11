using Lipar.Entities.Domain.General;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC;
using Microsoft.AspNetCore.Mvc;
using System;
using Lipar.Web.Framework.MVC.Filters;
using Lipar.Web.Areas.Admin.Helpers;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class LanguageController : BaseAdminController
    {
        #region Fields
        private readonly ILanguageModelFactory _languageModelFactory;
        private readonly ILanguageService _languageService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly ICommandService _commandService;
        private readonly IActivityLogService _activityLogService;
        #endregion

        #region Ctor
        public LanguageController(ILanguageModelFactory languageModelFactory
                                , ILanguageService languageService
                                , ILocaleStringResourceService localeStringResourceService
                                , ICommandService commandService
                                , IActivityLogService activityLogService)
        {
            _languageModelFactory = languageModelFactory;
            _languageService = languageService;
            _localeStringResourceService = localeStringResourceService;
            _commandService = commandService;
            _activityLogService = activityLogService;
        }
        #endregion

        #region Language Methods

        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult Index()
        => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult List()
        {
            var model = new LanguageSearchModel();
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult List(LanguageSearchModel searchModel)
        {
            var languageList = _languageModelFactory.PrepareLanguageListModel(searchModel);
            return Json(languageList);
        }

        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult Create()
        {
            var model = _languageModelFactory.PrepareLanguageModel(new LanguageModel(), null);
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult Create(LanguageModel model)
        {
            if (ModelState.IsValid)
            {
                var language = model.ToEntity<Language, int>();
                _languageService.Add(language);

                // add activity log for create language
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.Language.Create"), language);

                return RedirectToAction("List");
            }
            model = _languageModelFactory.PrepareLanguageModel(model, null);
            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult Edit(int Id)
        {
            var language = _languageService.GetById(Id);
            if (language == null)
                return RedirectToAction("List");

            var model = _languageModelFactory.PrepareLanguageModel(null, language);
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult Edit(LanguageModel model)
        {
            if (ModelState.IsValid)
            {
                var language = model.ToEntity<Language,int>();
                _languageService.Edit(language);

                // add activity log for edit language
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Language.Edit"), language);

                return RedirectToAction("Edit",new { Id = language.Id});
            }
            model = _languageModelFactory.PrepareLanguageModel(model, null);
            return View(model);
        }
        #endregion

        #region Resources

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult Resources(LocaleStringResourceSearchModel searchModel)
        {
            //prepare model
            var model = _languageModelFactory.PrepareLocaleStringResourceListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult ResourceAdd(int LanguageId, LocaleStringResourceModel model)
        {
            if (!string.IsNullOrEmpty(model.ResourceName))
                model.ResourceName = model.ResourceName.Trim();

            if (!string.IsNullOrEmpty(model.ResourceValue))
                model.ResourceValue = model.ResourceValue.Trim();

            var resourceName = _localeStringResourceService.GetByResourceName(model.ResourceName, LanguageId);

            if (resourceName == null)
            {
                var localeStringResource = model.ToEntity<LocaleStringResource, Guid>();

                localeStringResource.LanguageId = LanguageId;

                _localeStringResourceService.Add(localeStringResource);

                // add activity log for add local string resource
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.LocaleStringResource.Create"), localeStringResource);
            }
            else
                return ErrorJson(string.Format("exists"));

            return Json(new { Result = true });
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult ResourceDelete(Guid Id)
        {
            var localStringResource = _localeStringResourceService.GetById(Id);
            if(localStringResource == null)
                return new NullJsonResult();

           _localeStringResourceService.Delete(localStringResource);

            // add activity log for delete local string resource
            _activityLogService.Add("Admin.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.LocaleStringResource.Delete"), localStringResource);

            return new NullJsonResult();
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageLanguage)]
        public IActionResult ResourceEdit(LocaleStringResourceModel model)
        {

            var localeStringResource = _localeStringResourceService.GetById(model.Id)
                ?? throw new ArgumentNullException("no local string resource found with the specified id");

            localeStringResource.ResourceName = model.ResourceName;
            localeStringResource.ResourceValue = model.ResourceValue;

            _localeStringResourceService.Edit(localeStringResource);

            return new NullJsonResult();
        }
        #endregion
    }
}
