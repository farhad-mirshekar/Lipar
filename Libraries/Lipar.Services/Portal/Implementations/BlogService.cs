using Lipar.Core;
using Lipar.Data;
using Lipar.Entities;
using Lipar.Entities.Domain.Portal;
using Lipar.Entities.Domain.General;
using Lipar.Services.Portal.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class BlogService : IBlogService
    {
        #region Fields
        private readonly IRepository<Blog> _repository;
        private readonly IRepository<BlogMedia> _blogMediaRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public BlogService(IRepository<Blog> repository
                         , IWorkContext workContext
                         , IRepository<BlogMedia> blogMediaRepository)
        {
            _repository = repository;
            _workContext = workContext;
            _blogMediaRepository = blogMediaRepository;
        }
        #endregion

        #region Methods
        public void Add(Blog model)
        {
            model.UserId = _workContext.CurrentUser.Id;
            model.ModifiedDate = DateTime.Now;
            _repository.Insert(model);
        }

        public void Delete(Blog model)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(Blog model)
        {
            model.UserId = _workContext.CurrentUser.Id;
            model.ModifiedDate = DateTime.Now;

            _repository.Update(model);
        }

        public Blog GetById(int Id)
        {
            if (Id == 0)
                return null;

            var blog = _repository.GetById(Id);

            if (blog.RemoverId.HasValue && blog.RemoverId.Value != 0)
                return null;

            return blog;
        }

        public IPagedList<Blog> List(BlogListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                query = query.Where(b => b.Name.Contains(listVM.Name.Trim()));
            }
            if(listVM.UserId.HasValue && listVM.UserId.Value != 0)
            {
                query = query.Where(b => b.UserId == listVM.UserId);
            }

            var blogs = new PagedList<Blog>(query, listVM.PageIndex, listVM.PageSize);

            return blogs;
        }

        public BlogMedia GetPictureById(int Id)
        => _blogMediaRepository.GetById(Id);

        public void DeletePicture(int MediaId)
        {
            if (MediaId == 0)
                throw new Exception("");

            var query = _blogMediaRepository.Table;
            var blogMedia = query.Where(bm => bm.MediaId == MediaId).Single();

            _blogMediaRepository.Delete(blogMedia);
        }
        #endregion
    }
}
