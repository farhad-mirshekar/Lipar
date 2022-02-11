using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class BlogCommentService : IBlogCommentService
    {
        #region Fields
        private readonly IRepository<BlogComment> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public BlogCommentService(IRepository<BlogComment> repository
                                , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public void Add(BlogComment model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void Delete(BlogComment model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
        }

        public void Edit(BlogComment model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Update(model);
        }

        public int GetBlogCommentsCount(Guid blogId, CommentStatusEnum commentStatus)
        {
            var query = _repository.TableNoTracking.Where(bc => bc.BlogId == blogId);
            query = query.Where(bc => bc.CommentStatusId == (int)commentStatus);

            return query.Count();
        }

        public BlogComment GetById(Guid Id)
        {
            if (Id == Guid.Empty)
                throw new ArgumentNullException("blog model");

            return _repository.GetById(Id);
        }

        public IPagedList<BlogComment> List(BlogCommentListVM listVM)
        {

            var query = _repository.TableNoTracking;

            if (listVM.BlogId.HasValue && listVM.BlogId.Value != Guid.Empty)
            {
                query = query.Where(bc => bc.BlogId == listVM.BlogId);
            }

            if (listVM.ParentId.HasValue && listVM.ParentId.Value != Guid.Empty)
            {
                query = query.Where(bc => bc.ParentId == listVM.ParentId);
            }

            if (listVM.UserId.HasValue && listVM.UserId.Value != Guid.Empty)
            {
                query = query.Where(bc => bc.UserId == listVM.UserId);
            }

            if (listVM.CommentStatusId.HasValue && listVM.CommentStatusId.Value != 0)
            {
                query = query.Where(bc => bc.CommentStatusId == listVM.CommentStatusId);
            }

            var models = new PagedList<BlogComment>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
