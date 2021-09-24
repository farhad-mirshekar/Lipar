using Lipar.Core;
using Lipar.Entities;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    public interface IActivityLogService
    {
        void Add(string systemKeyword , string comment , BaseEntity entity = null);
        void Add(User user, string systemKeyword, string comment, BaseEntity entity = null);
        //void Edit(ActivityLog model);
        ActivityLog GetById(int Id);
        //void Delete(ActivityLog model);
        IPagedList<ActivityLog> List(ActivityLogListVM listVM);
    }
}
