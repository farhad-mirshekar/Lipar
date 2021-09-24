using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;

namespace Lipar.Services.Organization.Implementations
{
    public class PositionTypeService : IPositionTypeService
    {
        #region Ctor
        public PositionTypeService(IRepository<PositionType> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<PositionType> _repository;
        #endregion

        #region Methods
        public IPagedList<PositionType> List()
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<PositionType>(query);

            return models;
        }
        #endregion
    }
}
