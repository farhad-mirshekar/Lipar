using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Data;
using Lipar.Entities;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class ActivityLogService : IActivityLogService
    {
        #region Fields
        private readonly IRepository<ActivityLog> _repository;
        private readonly IActivityLogTypeService _activityLogTypeService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IWorkContext _workContext;
        
        #endregion

        #region Ctor
        public ActivityLogService(IRepository<ActivityLog> repository
                                , IActivityLogTypeService activityLogTypeService
                                , IStaticCacheManager staticCacheManager
                                , IWorkContext workContext)
        {
            _repository = repository;
            _activityLogTypeService = activityLogTypeService;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public void Add<T>(string systemKeyword, string comment, BaseEntity<T> entity = null)
        {
            Add<T>(_workContext.CurrentUser, systemKeyword, comment, entity);
        }

        public void Add<T>(User user, string systemKeyword, string comment, BaseEntity<T> entity = null)
        {
            if (user == null)
                return;

            var GetAllActivityLogTypeCached = GetsAllActivityLogType().FirstOrDefault(a => a.SystemKeyword.Contains(systemKeyword));

            var activityLog = new ActivityLog
            {
                ActivityLogTypeId = GetAllActivityLogTypeCached.Id,
                Comment = comment,
                UserId = user.Id,
                EntityId = entity?.Id.ToString(),
                EntityName = entity?.GetType().Name

            };

            _repository.Insert(activityLog);
        }

        public ActivityLog GetById(int Id)
        => _repository.GetById(Id);

        public IPagedList<ActivityLog> List(ActivityLogListVM listVM)
        {
            var query = _repository.Table;

            query = query.OrderByDescending(a => a.CreationDate);

            query = query.Include(a => a.ActivityLogType);

            var models = new PagedList<ActivityLog>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        #endregion

        #region Utilities
        private IEnumerable<ActivityLogType> GetsAllActivityLogType()
        {
            return _staticCacheManager.Get(LiparPublicDefaults.Activity_Log_Type_Get_All, () =>
            {
                return _activityLogTypeService.List(new ActivityLogTypeListVM { PageIndex = 0, PageSize = int.MaxValue });
            });
        }
        #endregion
    }
}
