using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class ActivityLogModelFactory : IActivityLogModelFactory
    {
        #region Fields
        private readonly IActivityLogService _activityLogService;
        #endregion

        #region Ctor
        public ActivityLogModelFactory(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }
        #endregion

        #region Methods
        public ActivityLogListModel PrepareActivityLogListModel(ActivityLogSearchModel searchModel)
        {
            var activityLogs = _activityLogService.List(new ActivityLogListVM { PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize });

            var model = new ActivityLogListModel().PrepareToGrid(searchModel, activityLogs, () =>
            {
                return activityLogs.Select(activityLog =>
                 {
                     var activityLogModel = activityLog.ToModel<ActivityLogModel>();
                     activityLogModel.ActivityLogType = activityLog.ActivityLogType.Name;
                     return activityLogModel;
                 });
            });

            return model;
        }
        #endregion
    }
}
