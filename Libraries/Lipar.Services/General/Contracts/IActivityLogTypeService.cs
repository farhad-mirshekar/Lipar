using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    public interface IActivityLogTypeService
    {
        void Add(ActivityLogType model);
        void Edit(ActivityLogType model);
        ActivityLogType GetById(int Id);
        IPagedList<ActivityLogType> List(ActivityLogTypeListVM listVM);
    }
}
