using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class ProductAttributeService : IProductAttributeService
    {
        #region Ctor
        public ProductAttributeService(IRepository<ProductAttribute> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ProductAttribute> _repository;
        #endregion

        #region Methods
        public void Add(ProductAttribute model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }

        public void Delete(ProductAttribute model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(ProductAttribute model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public ProductAttribute GetById(int Id)
        {
            if(Id == 0)
            {
                return null;
            }

            return _repository.GetById(Id);
        }

        public IPagedList<ProductAttribute> List(ProductAttributeListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                query = query.Where(p => p.Name.Contains(listVM.Name.Trim()));
            }

            var models = new PagedList<ProductAttribute>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
