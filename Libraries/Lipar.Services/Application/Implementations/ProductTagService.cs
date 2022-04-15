using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class ProductTagService : IProductTagService
    {
        #region Ctor
        public ProductTagService(IRepository<ProductTag> repository
                               , IRepository<ProductTagMapping> productTagMappingRepository)
        {
            _repository = repository;
            _productTagMappingRepository = productTagMappingRepository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ProductTag> _repository;
        private readonly IRepository<ProductTagMapping> _productTagMappingRepository;
        #endregion

        #region Methods

        public void Add(ProductTag model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.CreationDate = CommonHelper.GetCurrentDateTime();

            _repository.Insert(model);
        }

        public void Delete(ProductTag model)
        {
            throw new NotImplementedException();
        }

        public void Edit(ProductTag model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public ProductTag GetById(Guid id, bool noTracking = false)
        {
            if (id == Guid.Empty)
            {
                throw new Exception(nameof(id));
            }

            var query = _repository.Table;
            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            var model = query.Where(pt => pt.Id == id).FirstOrDefault();
            return model;
        }

        public IPagedList<ProductTag> List(ProductTagListVM listVM)
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<ProductTag>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        public void SaveProductTagMapping(IList<Guid> productTagIds, Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            var productTagMappings = _productTagMappingRepository.TableNoTracking
                                                                 .Where(ptm => ptm.ProductId == productId)
                                                                 .Select(ptm => ptm.ProductTagId)
                                                                 .ToList();

            var productTagForInsertNotExists = productTagIds.Except(productTagMappings);
            var productTagForDeleteNotExists = productTagMappings.Except(productTagIds);

            var productTagMappingsCreate = new List<ProductTagMapping>();
            var productTagMappingsRemove = new List<ProductTagMapping>();

            foreach (var productTagInsert in productTagForInsertNotExists)
            {
                productTagMappingsCreate.Add(new ProductTagMapping
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    ProductTagId = productTagInsert,
                    CreationDate = CommonHelper.GetCurrentDateTime(),
                });
            }

            foreach (var productTagRemove in productTagForDeleteNotExists)
            {
                var ptm = _productTagMappingRepository.Table
                                                      .Where(ptm => ptm.ProductTagId == productTagRemove)
                                                      .FirstOrDefault();
                productTagMappingsRemove.Add(ptm);
            }

            //add batch product tag
            AddProductTagMapping(productTagMappingsCreate);

            //remove batch product tag
            DeleteProductTagMapping(productTagMappingsRemove);
        }

        public IList<Guid> GetProductTagIds(Guid productId)
        {
            var productTags = _productTagMappingRepository.TableNoTracking;

            return productTags.Where(ptm => ptm.ProductId == productId)
                              .Select(pt => pt.ProductTagId)
                              .ToList();
        }

        public IList<ProductTagMapping> GetProductTagMappings(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                return null;
            }

            var query = _productTagMappingRepository.TableNoTracking;

            var productTags = query.Where(ptm => ptm.ProductId == productId)
                                   .Select(ptm => new ProductTagMapping
                                   {
                                       Id = ptm.Id,
                                       ProductId = ptm.ProductId,
                                       ProductTagId = ptm.ProductTagId,
                                       ProductTag = new ProductTag
                                       {
                                           Name = ptm.ProductTag.Name
                                       }
                                   }).ToList();

            return productTags;
        }
        #endregion

        #region Utilities

        protected void AddProductTagMapping(IList<ProductTagMapping> productTagMappings)
        {
            if (productTagMappings.Any())
            {
                _productTagMappingRepository.Insert(productTagMappings);
            }
        }
        protected void DeleteProductTagMapping(IList<ProductTagMapping> productTagMappings)
        {
            if (productTagMappings.Any())
            {
                _productTagMappingRepository.Delete(productTagMappings);
            }
        }
        #endregion
    }
}
