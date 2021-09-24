using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Core;
using Lipar.Services.Core.Contracts;

namespace Lipar.Services.Core.Implementations
{
    public class EnabledTypeService : IEnabledTypeService
    {
        #region Ctor
        public EnabledTypeService(IRepository<EnabledType> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<EnabledType> _repository;
        #endregion

        #region Methods
        public IPagedList<EnabledType> List()
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<EnabledType>(query);

            return models;
        }
        #endregion
    }
}
