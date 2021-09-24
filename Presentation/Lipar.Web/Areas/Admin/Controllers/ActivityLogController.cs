using Lipar.Web.Areas.Admin.Factories.General;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class ActivityLogController : BaseAdminController
    {
        #region Fields
        private readonly IActivityLogModelFactory _activityLogModelFactory;
        #endregion

        #region Ctor
        public ActivityLogController(IActivityLogModelFactory activityLogModelFactory)
        {
            _activityLogModelFactory = activityLogModelFactory;
        }
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List()
            => View(new ActivityLogSearchModel());

        [HttpPost]
        public IActionResult List(ActivityLogSearchModel searchModel)
        {
            var model = _activityLogModelFactory.PrepareActivityLogListModel(searchModel);
            
            return Json(model);
        }
        #endregion
    }
}
