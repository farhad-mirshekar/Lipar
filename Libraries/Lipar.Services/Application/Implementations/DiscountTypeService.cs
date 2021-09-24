using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;

namespace Lipar.Services.Application.Implementations
{
    public class DiscountTypeService : IDiscountTypeService
    {
        #region Ctor
        public DiscountTypeService(IRepository<DiscountType> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<DiscountType> _repository;
        #endregion

        #region Methods
        public IPagedList<DiscountType> List()
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<DiscountType>(query);

            return models;
        }
        #endregion
    }
}
