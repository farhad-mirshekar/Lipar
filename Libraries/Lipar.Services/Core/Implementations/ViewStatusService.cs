using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Core;
using Lipar.Services.Core.Contracts;

namespace Lipar.Services.Core.Implementations
{
    public class ViewStatusService : IViewStatusService
    {
        #region Ctor
        public ViewStatusService(IRepository<ViewStatus> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ViewStatus> _repository;
        #endregion

        #region Methods
        public IPagedList<ViewStatus> List()
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<ViewStatus>(query);

            return models;
        }
        #endregion
    }
}
