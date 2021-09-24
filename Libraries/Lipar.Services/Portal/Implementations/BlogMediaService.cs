using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class BlogMediaService : IBlogMediaService
    {
        #region Fields
        private readonly IRepository<BlogMedia> _repository;
        private readonly IRepository<Blog> _blogRepository;
        #endregion

        #region Ctor
        public BlogMediaService(IRepository<BlogMedia> repository
                              , IRepository<Blog> blogRepository)
        {
            _repository = repository;
            _blogRepository = blogRepository;
        }
        #endregion

        #region Methods
        public void Delete(BlogMedia blogMedia)
        {
            if (blogMedia == null)
                throw new ArgumentNullException(nameof(blogMedia));

            _repository.Delete(blogMedia);

        }

        public IPagedList<BlogMedia> List(BlogMediaListVM listVM)
        {
            var query = from bm in _repository.Table
                        join b in _blogRepository.Table on bm.BlogId equals b.Id
                        select bm;

            if (listVM.BlogId.HasValue && listVM.BlogId.Value > 0)
                query = query.Where(x => x.BlogId == listVM.BlogId.Value);

            if (listVM.MediaId.HasValue && listVM.MediaId.Value > 0)
                query = query.Where(x => x.MediaId == listVM.MediaId.Value);

            var blogMediaList = new PagedList<BlogMedia>(query, listVM.PageIndex, listVM.PageSize);

            return blogMediaList;
        }

        public void Save(BlogMedia blogMedia)
        {
            var query = _repository.Table;

            var blogMediaMap = query.Where(bm => bm.BlogId == blogMedia.BlogId && bm.MediaId == blogMedia.MediaId).FirstOrDefault();

            if (blogMediaMap == null)
                _repository.Insert(blogMedia);
            else
                _repository.Update(blogMedia);
        }
        #endregion
    }
}
