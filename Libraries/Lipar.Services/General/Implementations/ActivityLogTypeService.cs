using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;

namespace Lipar.Services.General.Implementations
{
    public class ActivityLogTypeService : IActivityLogTypeService
    {
        #region Fields
        private readonly IRepository<ActivityLogType> _repository;
        #endregion

        #region Ctor
        public ActivityLogTypeService(IRepository<ActivityLogType> repository)
        {
            _repository = repository;
        }
        #endregion
        public void Add(ActivityLogType model)
        => _repository.Insert(model);

        public void Edit(ActivityLogType model)
       => _repository.Update(model);

        public ActivityLogType GetById(int Id)
        => _repository.GetById(Id);

        public IPagedList<ActivityLogType> List(ActivityLogTypeListVM listVM)
        {
            var query = _repository.Table;

            var models = new PagedList<ActivityLogType>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
    }
}
