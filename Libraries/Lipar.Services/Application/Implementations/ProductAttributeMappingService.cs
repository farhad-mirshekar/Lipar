using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class ProductAttributeMappingService : IProductAttributeMappingService
    {
        #region Ctor
        public ProductAttributeMappingService(IRepository<ProductAttributeMapping> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ProductAttributeMapping> _repository;
        #endregion

        #region Methods
        public IPagedList<ProductAttributeMapping> List(ProductAttributeMappingListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (listVM.ProductId.HasValue && listVM.ProductId.Value != 0)
            {
                query = query.Where(p => p.ProductId == listVM.ProductId);
            }

            if (listVM.AttributeId.HasValue && listVM.AttributeId.Value != 0)
            {
                query = query.Where(p => p.ProductId == listVM.AttributeId);
            }

            var models = new PagedList<ProductAttributeMapping>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        public void Add(ProductAttributeMapping model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }

        public void Edit(ProductAttributeMapping model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public void Delete(ProductAttributeMapping model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public ProductAttributeMapping GetById(int Id)
        {
            if (Id == 0)
            {
                return null;
            }

            return _repository.GetById(Id);
        }
        #endregion
    }
}
