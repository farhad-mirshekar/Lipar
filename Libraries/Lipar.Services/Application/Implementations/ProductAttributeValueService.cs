using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class ProductAttributeValueService : IProductAttributeValueService
    {
        #region Ctor
        public ProductAttributeValueService(IRepository<ProductAttributeValue> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ProductAttributeValue> _repository;
        #endregion

        #region Methods
        public void Add(ProductAttributeValue model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }

        public void Delete(ProductAttributeValue model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(ProductAttributeValue model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public ProductAttributeValue GetById(int Id)
        {
            if (Id == 0)
            {
                return null;
            }

           return _repository.GetById(Id);
        }

        public IPagedList<ProductAttributeValue> List(ProductAttributeValueListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if(listVM.ProductAttributeMappingId.HasValue && listVM.ProductAttributeMappingId.Value != 0)
            {
                query = query.Where(p => p.ProductAttributeMappingId == listVM.ProductAttributeMappingId);
            }

            var models = new PagedList<ProductAttributeValue>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
