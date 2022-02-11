using Lipar.Core;
using Lipar.Entities;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    public interface IActivityLogService
    {
        void Add<T>(string systemKeyword , string comment , BaseEntity<T> entity = null);
        void Add<T>(User user, string systemKeyword, string comment, BaseEntity<T> entity = null);
        //void Edit(ActivityLog model);
        ActivityLog GetById(int Id);
        //void Delete(ActivityLog model);
        IPagedList<ActivityLog> List(ActivityLogListVM listVM);
    }
}
