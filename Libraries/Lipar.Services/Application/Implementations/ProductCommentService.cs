using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class ProductCommentService : IProductCommentService
    {
        #region Ctor
        public ProductCommentService(IRepository<ProductComment> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<ProductComment> _repository;
        #endregion

        #region Methods
        public void Add(ProductComment model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }

        public void Delete(ProductComment model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(ProductComment model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public ProductComment GetById(Guid Id, bool noTracking = false)
        {
            if (Id == Guid.Empty)
            {
                return null;
            }

            if (!noTracking)
            {
                var query = _repository.TableNoTracking;

                var productComment = query.FirstOrDefault(pc => pc.Id == Id);

                return productComment;
            }

            return _repository.GetById(Id);
        }

        public IPagedList<ProductComment> List(ProductCommentListVM listVM)
        {
            var query = _repository.TableNoTracking
                                    .Include(pc => pc.Product)
                                    .Include(pc => pc.User)
                                    .Include(pc => pc.Parent)
                                    .Include(pc => pc.CommentStatus)
                                    .Include(pc => pc.Children).ThenInclude(pc => pc.User).AsQueryable();

            if (listVM.ProductId.HasValue && listVM.ProductId.Value != Guid.Empty)
            {
                query = query.Where(pc => pc.ProductId == listVM.ProductId);
            }

            if (listVM.ParentId.HasValue && listVM.ParentId.Value != Guid.Empty)
            {
                query = query.Where(pc => pc.ParentId == listVM.ParentId);
            }

            if (listVM.UserId.HasValue && listVM.UserId.Value != Guid.Empty)
            {
                query = query.Where(pc => pc.UserId == listVM.UserId);
            }

            if (listVM.CommentStatusId.HasValue && listVM.CommentStatusId.Value != 0)
            {
                query = query.Where(pc => pc.CommentStatusId == listVM.CommentStatusId.Value);
            }

            var models = new PagedList<ProductComment>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        public IQueryable<ProductComment> ProductCommentQuery(bool noTracking = false)
        {
            var query = _repository.Table;

            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            return query;
        }
        #endregion
    }
}
