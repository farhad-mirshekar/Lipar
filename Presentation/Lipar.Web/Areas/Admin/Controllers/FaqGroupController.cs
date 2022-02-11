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
    public class FaqGroupController : BaseAdminController
    {
        #region Fields
        private readonly IFaqGroupModelFactory _faqGroupModelFactory;
        private readonly IFaqGroupService _faqGroupService;
        private readonly IFaqService _faqService;
        private readonly ICommandService _commandService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Ctor
        public FaqGroupController(IFaqGroupModelFactory faqGroupModelFactory
                                , IFaqGroupService faqGroupService
                                , IFaqService faqService
                                , ICommandService commandService
                                , IActivityLogService activityLogService
                                , ILocaleStringResourceService localeStringResourceService)
        {
            _faqGroupModelFactory = faqGroupModelFactory;
            _faqGroupService = faqGroupService;
            _faqService = faqService;
            _commandService = commandService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region faq Group Methods

        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult Index()
            => RedirectToAction("List");

        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult List()
            => View(new FaqGroupSearchModel());

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult List(FaqGroupSearchModel searchModel)
        {
            var model = _faqGroupModelFactory.PrepareFaqGroupListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult Create()
        {
            var model = _faqGroupModelFactory.PrepareFaqGroupModel(new FaqGroupModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult Create(FaqGroupModel model)
        {
            if (ModelState.IsValid)
            {
                var faqGroup = model.ToEntity<FaqGroup, Guid>();

                _faqGroupService.Add(faqGroup);

                //add activity log for create faq group
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.FaqGroup.Create"), faqGroup);

                //success
                return RedirectToAction("Edit", new { Id = faqGroup.Id });
            }
            model = _faqGroupModelFactory.PrepareFaqGroupModel(new FaqGroupModel(), null);

            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult Edit(Guid Id)
        {
            if (Id == Guid.Empty)
                return RedirectToAction("List");

            var faqGroup = _faqGroupService.GetById(Id);
            if (faqGroup == null)
                return RedirectToAction("List");

            var model = _faqGroupModelFactory.PrepareFaqGroupModel(null, faqGroup);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult Edit(FaqGroupModel model)
        {
            if (ModelState.IsValid)
            {
                var faqGroup = model.ToEntity<FaqGroup, Guid>();

                _faqGroupService.Edit(faqGroup);

                //add activity log for edit faq group
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.FaqGroup.Edit"), faqGroup);

                //success
                return RedirectToAction("Edit", new { Id = faqGroup.Id });
            }
            model = _faqGroupModelFactory.PrepareFaqGroupModel(new FaqGroupModel(), null);

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult Delete(Guid Id)
        {
            if (Id == Guid.Empty)
                return RedirectToAction("List");

            var faqGroup = _faqGroupService.GetById(Id);
            if (faqGroup == null)
                return RedirectToAction("List");

            _faqGroupService.Delete(faqGroup);

            //add activity log for delete faq group
            _activityLogService.Add("Admin.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.FaqGroup.Delete"), faqGroup);

            return RedirectToAction("List");

        }
        #endregion

        #region Faq Methods

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult FaqList(FaqSearchModel searchModel)
        {
            var model = _faqGroupModelFactory.PrepareFaqListModel(searchModel);

            return Json(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult FaqEdit(Guid Id, int faqGroupId)
        {
            if (Id == Guid.Empty)
                return RedirectToAction("Edit", new { Id = faqGroupId });

            var faq = _faqService.GetById(Id);
            if (faq == null)
                return RedirectToAction("List");

            var model = _faqGroupModelFactory.PrepareFaqModel(null, faq);
            return View(model);

        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult FaqEdit(FaqModel model)
        {
            if (ModelState.IsValid)
            {
                var faq = model.ToEntity<Faq, Guid>();

                _faqService.Edit(faq);

                //add activity log for edit faq
                _activityLogService.Add("Admin.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Faq.Edit"), faq);

                return RedirectToAction("Edit", new { Id = faq.FaqGroupId });
            }

            model = _faqGroupModelFactory.PrepareFaqModel(model, null);
            return View(model);
        }

        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult FaqCreate(Guid FaqGroupId)
        {
            var model = _faqGroupModelFactory.PrepareFaqModel(new FaqModel(), null);
            model.FaqGroupId = FaqGroupId;

            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: "ManageEmailAccount")]
        public IActionResult FaqCreate(FaqModel model)
        {
            if (ModelState.IsValid)
            {
                var faq = model.ToEntity<Faq, Guid>();

                _faqService.Add(faq);

                //add activity log for edit faq
                _activityLogService.Add("Admin.Add", _localeStringResourceService.GetResource("ActivityLog.Admin.Faq.Create"), faq);

                return RedirectToAction("Edit", new { Id = faq.FaqGroupId });
            }

            model = _faqGroupModelFactory.PrepareFaqModel(model, null);
            return View(model);
        }

        [HttpPost]
        [CheckingPermissions(permissions: CommandNames.ManageFaqGroup)]
        public IActionResult FaqDelete(Guid Id)
        {
            if(Id == Guid.Empty)
                return new NullJsonResult();

            var faq = _faqService.GetById(Id);
            if (faq == null)
                return new NullJsonResult();

            _faqService.Delete(faq);

            return new NullJsonResult();
        }
        #endregion
    }
}
