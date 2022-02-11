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
    public class ProductAnswersService : IProductAnswersService
    {
        #region Ctor
        public ProductAnswersService(IRepository<ProductAnswers> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ProductAnswers> _repository;
        #endregion

        #region Methods
        public void Add(ProductAnswers model)
        {
            if(model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }

        public void Delete(ProductAnswers model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(ProductAnswers model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public ProductAnswers GetById(Guid Id, bool noTracking = false)
        {
            if (Id == Guid.Empty)
            {
                return null;
            }

            if (!noTracking)
            {
                var query = _repository.TableNoTracking;

                var productQuestion = query.FirstOrDefault(pa => pa.Id == Id);

                return productQuestion;
            }

            return _repository.GetById(Id);
        }

        public IPagedList<ProductAnswers> List(ProductAnswersListVM listVM)
        {
            var query = _repository.TableNoTracking
                                    .Include(pq => pq.ProductQuestion)
                                    .Include(pq => pq.User).AsQueryable();

            if (listVM.ProductQuestionId.HasValue && listVM.ProductQuestionId.Value != Guid.Empty)
            {
                query = query.Where(pa => pa.ProductQuestionId == listVM.ProductQuestionId);
            }

            if (listVM.UserId.HasValue && listVM.UserId.Value != Guid.Empty)
            {
                query = query.Where(pa => pa.UserId == listVM.UserId);
            }

            if (listVM.ViewStatusId.HasValue && listVM.ViewStatusId.Value != 0)
            {
                query = query.Where(pa => pa.ViewStatusId == listVM.ViewStatusId);
            }

            var models = new PagedList<ProductAnswers>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
