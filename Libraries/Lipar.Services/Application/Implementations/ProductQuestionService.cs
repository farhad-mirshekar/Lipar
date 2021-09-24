using Lipar.Core;
using Lipar.Data;
using Lipar.Entities;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class ProductQuestionService : IProductQuestionService
    {
        #region Ctor
        public ProductQuestionService(IRepository<ProductQuestion> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ProductQuestion> _repository;
        #endregion

        #region Methods
        public void Add(ProductQuestion model)
        {
            if(model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }

        public void Delete(ProductQuestion model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(ProductQuestion model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public ProductQuestion GetById(int Id, bool noTracking = false)
        {
            if(Id == 0)
            {
                return null;
            }

           var query = _repository.Table;
            if (noTracking)
            {
                 query = _repository.TableNoTracking;

                var productQuestion = query.FirstOrDefault(pq => pq.Id == Id);

                return productQuestion;
            }

             query = query.Include(p => p.Product)
                          .Include(u => u.User);

            return query.FirstOrDefault(x => x.Id == Id);
        }

        public IPagedList<ProductQuestion> List(ProductQuestionListVM listVM)
        {
            var query = _repository.TableNoTracking
                                   .Include(pq => pq.ProductAnswers)
                                   .Include(pq => pq.Product)
                                   .Include(pq => pq.User).AsQueryable();

            if(listVM.ProductId.HasValue && listVM.ProductId.Value != 0)
            {
                query = query.Where(pq => pq.ProductId == listVM.ProductId);
            }

            if (listVM.UserId.HasValue && listVM.UserId.Value != 0)
            {
                query = query.Where(pq => pq.UserId == listVM.UserId);
            }

            if (listVM.ViewStatusId.HasValue && listVM.ViewStatusId.Value != 0)
            {
                query = query.Where(pq => pq.ViewStatusId == listVM.ViewStatusId);
            }

            var models = new PagedList<ProductQuestion>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
