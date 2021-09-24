using Lipar.Core.Caching;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories;
using Lipar.Web.Areas.Admin.Infrastructure.Cache;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class CommandController : BaseAdminController
    {
        #region Fields
        private readonly ICommandModelFactory _commandModelFactory;
        private readonly ICommandService _commandService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Ctor
        public CommandController(ICommandModelFactory commandModelFactory
                               , ICommandService commandService
                               , IStaticCacheManager staticCacheManager
                               , IActivityLogService activityLogService
                               , ILocaleStringResourceService localeStringResourceService)
        {
            _commandModelFactory = commandModelFactory;
            _commandService = commandService;
            _staticCacheManager = staticCacheManager;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Command Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new CommandSearchModel());

        [HttpPost]
        public IActionResult List(CommandSearchModel searchModel)
        {
            if (!_commandService.CheckPermission("ManageCommand"))
                return AccessDeniedDataTablesJson();

            var model = _commandModelFactory.PrepareCommandListModel(searchModel);
            return Json(model);
        }

        public IActionResult Edit(int Id)
        {
            if (!_commandService.CheckPermission("ManageCommand"))
                return AccessDeniedView();

            var command = _commandService.GetById(Id);
            if (command == null)
                return RedirectToAction("List");

            var model = _commandModelFactory.PrepareCommandModel(null, command);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CommandModel model)
        {
            if (!_commandService.CheckPermission("ManageCommand"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var command = model.ToEntity<Command>();
                command.ParentId = command.ParentId.HasValue && command.ParentId.Value == 0 ? null : command.ParentId;

                 _commandService.Edit(command);

                // add activity log for edit command
                _activityLogService.Add("Admin.Command.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.Command.Edit"), command);

                //remove cache
                _staticCacheManager.Remove(LiparModelCacheDefaults.Command_List_Key);

                //success add
                return RedirectToAction("Edit", new { Id = command.Id });
            }

            model =  _commandModelFactory.PrepareCommandModel(model, null);
            return View(model);
        }

        public IActionResult Create()
        {
            if (!_commandService.CheckPermission("ManageCommand"))
                return AccessDeniedView();

            var model = _commandModelFactory.PrepareCommandModel(new CommandModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CommandModel model)
        {
            if (!_commandService.CheckPermission("ManageCommand"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var command = model.ToEntity<Command>();
                command.ParentId = command.ParentId.HasValue && command.ParentId.Value == 0 ? null : command.ParentId;

                _commandService.Add(command);

                // add activity log for create command
                _activityLogService.Add("Admin.Command.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.Command.Create"), command);

                //remove cache
                _staticCacheManager.Remove(LiparModelCacheDefaults.Command_List_Key);

                //success add
                return RedirectToAction("Edit", new { Id = command.Id });
            }

            model = _commandModelFactory.PrepareCommandModel(model, null);
            return View(model);
        }

        public IActionResult Delete(int Id)
        {
            var command = _commandService.GetById(Id);
            if (command == null)
                return RedirectToAction("List");

            _commandService.Delete(command);

            // add activity log for delete command
            _activityLogService.Add("Admin.Command.Delete", _localeStringResourceService.GetResource("ActivityLog.Admin.Command.Delete"), command);

            return new NullJsonResult();
        }
        #endregion
    }
}
