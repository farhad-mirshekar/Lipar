using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class ProductMediaService : IProductMediaService
    {
        #region Ctor
        public ProductMediaService(IRepository<ProductMedia> repository
                                 , IRepository<Product> productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ProductMedia> _repository;
        private readonly IRepository<Product> _productRepository;
        #endregion

        #region Methods
        public void Delete(ProductMedia model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public IPagedList<ProductMedia> List(ProductMediaListVM listVM)
        {
            var query = from pm in _repository.TableNoTracking
                        join p in _productRepository.TableNoTracking on pm.ProductId equals p.Id
                        select pm;

            if(listVM.ProductId.HasValue && listVM.ProductId.Value != 0)
            {
                query = query.Where(pm => pm.ProductId == listVM.ProductId);
            }

            var models = new PagedList<ProductMedia>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        public void Save(ProductMedia model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var query = _repository.TableNoTracking;

            var productMedia = query.Where(pm => pm.ProductId == model.ProductId && pm.MediaId == model.MediaId).FirstOrDefault();

            if(productMedia == null)
            {
                Add(model);
            }
            else
            {
                Edit(model);
            }
        }
        #endregion

        #region Utilities
        protected void Add(ProductMedia model)
            => _repository.Insert(model);

        protected void Edit(ProductMedia model)
            => _repository.Update(model);

        public ProductMedia GetById(int Id)
        {
            if(Id == 0)
            {
                return null;
            }

            return _repository.GetById(Id);
        }

        public ProductMedia GetByMediaId(int mediaId)
        {
            if (mediaId == 0)
            {
                return null;
            }

            var productMedia = _repository.TableNoTracking
                                    .Where(m => m.MediaId == mediaId).FirstOrDefault();

            return productMedia;
        }
        #endregion
    }
}
