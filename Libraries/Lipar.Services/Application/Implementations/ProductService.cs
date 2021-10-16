﻿using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class ProductService : IProductService
    {
        #region Ctor
        public ProductService(IRepository<Product> repository
                            , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IRepository<Product> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Methods
        public void Add(Product model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.UserId = _workContext.CurrentUser.Id;
            model.UpdatedDate = CommonHelper.GetCurrentDateTime();

            _repository.Insert(model);
        }

        public void Delete(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.RemoverId = _workContext.CurrentUser.Id;
            model.RemoveDate = CommonHelper.GetCurrentDateTime();

            model.Published = false;

            Edit(model);
        }

        public void Edit(Product model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.UserId = _workContext.CurrentUser.Id;
            model.UpdatedDate = CommonHelper.GetCurrentDateTime();

            _repository.Update(model);
        }

        public Product GetById(int Id, bool noTracking = false)
        {
            if (Id == 0)
            {
                return null;
            }
            IQueryable<Product> query;

            if (noTracking)
            {
                query = _repository.TableNoTracking
                                    .Include(p => p.ProductQuestions).ThenInclude(pq => pq.ProductAnswers).ThenInclude(pq => pq.User)
                                    .Include(p => p.ProductQuestions).ThenInclude(pq => pq.User)
                                    .Include(p => p.ProductAttributeMappings);

                var product = query.FirstOrDefault(pc => pc.Id == Id);

                return product;
            }

            query = _repository.Table
                               .Include(p => p.ProductQuestions);

            return query.FirstOrDefault(p => p.Id == Id);
        }

        public IPagedList<Product> List(ProductListVM listVM)
        {
            var query = _repository.TableNoTracking;

            query = query.Where(p => !p.RemoverId.HasValue);

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                query = query.Where(p => p.Name.Contains(listVM.Name.Trim()));
            }

            //switch (listVM.ProductSortingType)
            //{
            //    case Entities.ProductSortingType.CreationDateAsc:
            //        query = query.OrderBy(p => p.CreationDate);
            //        break;
            //    case Entities.ProductSortingType.CreationDateDesc:
            //        query = query.OrderByDescending(p => p.CreationDate);
            //        break;
            //    case Entities.ProductSortingType.NameAsc:
            //        query = query.OrderBy(p => p.Name);
            //        break;
            //    case Entities.ProductSortingType.NameDesc:
            //        query = query.OrderByDescending(p => p.Name);
            //        break;
            //}

            var models = new PagedList<Product>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        public IEnumerable<Product> GetByIds(int[] Ids)
        {
            if (Ids == null || Ids.Length == 0)
            {
                return new List<Product>();
            }

            var query = _repository.TableNoTracking;

            query = query.Where(p => Ids.Contains(p.Id) && p.RemoverId == null);

            var sortedProduct = new List<Product>();

            foreach (var id in Ids)
            {
                var product = query.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    sortedProduct.Add(product);
                }
            }

            return sortedProduct;
        }

        public string GetProductName(int Id)
        {
            if (Id == 0)
            {
                return "";
            }

            var query = _repository.TableNoTracking.Where(x => x.Id == Id).Select(x => x.Name);

            return query.FirstOrDefault();
        }
        #endregion
    }
}
