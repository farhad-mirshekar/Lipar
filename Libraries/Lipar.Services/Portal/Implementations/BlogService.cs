using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using System;
using System.Linq;
using Lipar.Services.General.Contracts;
using Lipar.Core.Caching;

namespace Lipar.Services.Portal.Implementations
{
    public class BlogService : IBlogService
    {
        #region Ctor
        public BlogService(IRepository<Blog> repository
                         , IWorkContext workContext
                         , IRepository<BlogMedia> blogMediaRepository
                         , ISettingService settingService
                         , IStaticCacheManager cacheManager)
        {
            _repository = repository;
            _workContext = workContext;
            _blogMediaRepository = blogMediaRepository;
            _settingService = settingService;
            _cacheManager = cacheManager;
        }
        #endregion

        #region Fields
        private readonly IRepository<Blog> _repository;
        private readonly IRepository<BlogMedia> _blogMediaRepository;
        private readonly IWorkContext _workContext;
        private readonly ISettingService _settingService;
        private readonly IStaticCacheManager _cacheManager;
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

        public Blog GetById(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            var blog = _repository.GetById(Id);

            if (blog.RemoverId.HasValue && blog.RemoverId.Value != Guid.Empty)
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
            if(listVM.UserId.HasValue && listVM.UserId.Value != Guid.Empty)
            {
                query = query.Where(b => b.UserId == listVM.UserId);
            }

            var blogs = new PagedList<Blog>(query, listVM.PageIndex, listVM.PageSize);

            return blogs;
        }

        public BlogMedia GetPictureById(int Id)
        => _blogMediaRepository.GetById(Id);

        public void DeletePicture(Guid MediaId)
        {
            if (MediaId == Guid.Empty)
                throw new Exception("");

            var query = _blogMediaRepository.Table;
            var blogMedia = query.Where(bm => bm.MediaId == MediaId).Single();

            _blogMediaRepository.Delete(blogMedia);
        }

        public BlogSetting BlogSettings()
        {
            var cacheKey = LiparPortalDefaults.Load_Blog_Settings;
            return _cacheManager.Get(cacheKey, () =>
            {
                return _settingService.LoadSettings<BlogSetting>();
            });
        }
        #endregion
    }
}
