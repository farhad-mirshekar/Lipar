using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;

namespace Lipar.Services.Application.Implementations
{
    public class AttributeControlTypeService : IAttributeControlTypeService
    {
        #region Ctor
        public AttributeControlTypeService(IRepository<AttributeControlType> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<AttributeControlType> _repository;
        #endregion

        #region Methods
        public IPagedList<AttributeControlType> List()
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<AttributeControlType>(query);

            return models;
        }
        #endregion
    }
}
