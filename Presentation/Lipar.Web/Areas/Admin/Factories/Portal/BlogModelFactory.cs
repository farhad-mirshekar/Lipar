using Lipar.Core;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public class BlogModelFactory : IBlogModelFactory
    {
        #region Fields
        private readonly IBlogService _blogService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IMediaService _mediaService;
        private readonly IBlogMediaService _blogMediaService;
        private readonly IBlogCommentService _blogCommentService;
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public BlogModelFactory(IBlogService blogService
                              , IBaseAdminModelFactory baseAdminModelFactory
                              , IMediaService mediaService
                              , IBlogMediaService blogMediaService
                              , IBlogCommentService blogCommentService
                              , IUserService userService
                              , IWorkContext workContext)
        {
            _blogService = blogService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _mediaService = mediaService;
            _blogMediaService = blogMediaService;
            _blogCommentService = blogCommentService;
            _userService = userService;
            _workContext = workContext;
        }

        #endregion

        #region Methods
        public BlogListModel PrepareBlogListModel(BlogSearchModel searchModel)
        {
            var blogs = _blogService.List(new BlogListVM
            {
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
                UserId = _workContext.CurrentUser.Id,
            });

            var model = new BlogListModel().PrepareToGrid(searchModel, blogs, () =>
            {
                return blogs.Select(blog =>
                 {
                     var blogModel = blog.ToModel<BlogModel>();

                     blogModel.ApprovedComments = _blogCommentService.GetBlogCommentsCount(blog.Id, CommentStatusEnum.Open);
                     blogModel.NotApprovedComments = _blogCommentService.GetBlogCommentsCount(blog.Id, CommentStatusEnum.Close);

                     return blogModel;
                 });
            });

            return model;
        }

        public BlogMediaListModel PrepareBlogMediaListModel(BlogMediaSearchModel searchModel)
        {
            var blogMedia = _blogMediaService.List(new BlogMediaListVM { BlogId = searchModel.BlogId, PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize });

            var model = new BlogMediaListModel().PrepareToGrid(searchModel, blogMedia, () =>
            {
                return blogMedia.Select(bm =>
                 {
                     var blogMedia = bm.ToModel<BlogMediaModel>();

                     var mediaResult = _mediaService.GetById(blogMedia.MediaId);
                     if (mediaResult == null)
                         throw new Exception(nameof(mediaResult));

                     blogMedia.AltAttribute = mediaResult.AltAttribute;
                     //blogMedia.Priority = mediaResult.Data.Priority;
                     blogMedia.Name = mediaResult.Name;

                     return blogMedia;
                 });
            });

            return model;
        }

        public BlogModel PrepareBlogModel(BlogModel model, Blog blog)
        {
            if (blog != null)
            {
                model ??= blog.ToModel<BlogModel>();
                PrepareBlogMediaSearchModel(model.BlogMediaSearchModel, blog);
            }

            _baseAdminModelFactory.PrepareViewStatusType(model.AvailableViewStatusType);
            _baseAdminModelFactory.PrepareCommentStatusType(model.AvailableCommentStatusType);
            _baseAdminModelFactory.PrepareAllLanguage(model.AvailableLanguageType);
            model.AvailableCategoryType = _baseAdminModelFactory.PrepareCategoriesForPortal();


            return model;
        }

        public BlogCommentSearchModel PrepareBlogCommentSearchModel(BlogCommentSearchModel searchModel, Blog blog)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.BlogId = blog?.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }
        public BlogCommentListModel PrepareBlogCommentListModel(BlogCommentSearchModel searchModel)
        {
            var blogComments = _blogCommentService.List(new BlogCommentListVM { BlogId = searchModel.BlogId, PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize });

            var model = new BlogCommentListModel().PrepareToGrid(searchModel, blogComments, () =>
            {
                return blogComments.Select(blogComment =>
                {
                    var blogCommentModel = blogComment.ToModel<BlogCommentModel>();
                    var user = _userService.GetById(blogComment.UserId);

                    blogCommentModel.CreatorFullName = $"{user.FirstName} {user.LastName}";
                    return blogCommentModel;
                });
            });

            return model;
        }

        public BlogCommentModel PrepareBlogCommentModel(BlogCommentModel model, BlogComment blogComment)
        {
            if (blogComment != null)
                model = blogComment.ToModel<BlogCommentModel>();

            _baseAdminModelFactory.PrepareCommentStatusType(model.AvailableCommentStatusType);

            return model;
        }
        #endregion

        #region Utilities
        protected BlogMediaSearchModel PrepareBlogMediaSearchModel(BlogMediaSearchModel searchModel, Blog blog)
        {
            if (searchModel == null)
                throw new Exception(nameof(searchModel));

            if (blog == null)
                throw new Exception(nameof(blog));

            searchModel.BlogId = blog.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }
        #endregion
    }
}
