using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class RelatedProductService : IRelatedProductService
    {
        #region Ctor
        public RelatedProductService(IRepository<RelatedProduct> repository
                                   , IRepository<Product> productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }
        #endregion

        #region Fields
        private readonly IRepository<RelatedProduct> _repository;
        private readonly IRepository<Product> _productRepository;
        #endregion

        #region Methods
        public void Add(RelatedProduct model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }

        public void Delete(RelatedProduct model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(RelatedProduct model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public RelatedProduct GetById(int Id)
        {
            if (Id == 0)
            {
                return null;
            }

            return _repository.GetById(Id);
        }

        public IPagedList<RelatedProduct> List(RelatedProductListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (listVM.ProductId1.HasValue && listVM.ProductId1.Value > 0)
            {
                query = query.Where(r => r.ProductId1 == listVM.ProductId1);
            }

            var models = new PagedList<RelatedProduct>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        public IEnumerable<RelatedProduct> GetRelatedProductsByProductId1(int productId1)
        {
            var query = from r in _repository.TableNoTracking
                        join p in _productRepository.TableNoTracking on r.ProductId1 equals p.Id
                        where p.RemoverId == null && r.ProductId1 == productId1
                        orderby r.Priority
                        select r;

            return query;
        }

        public RelatedProduct FindRelatedProduct(IEnumerable<RelatedProduct> source, int productId1, int productId2)
        {
            foreach (var relatedProduct in source)
            {
                if (relatedProduct.ProductId1 == productId1 && relatedProduct.ProductId2 == productId2)
                {
                    return relatedProduct;
                }
            }

            return null;
        }
        #endregion
    }
}
