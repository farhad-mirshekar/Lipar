using Lipar.Core.Common;
using Lipar.Core.ReadingTime;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.Portal.Contracts;
using Lipar.Web.Framework.Models;
using Lipar.Web.Models.Portal;
using System;
using System.Linq;

namespace Lipar.Web.Factories.Portal
{
    public class BlogModelFactory : IBlogModelFactory
    {
        private readonly IBlogService _blogService;
        private readonly IBlogMediaService _blogMediaService;
        private readonly IBlogCommentService _blogCommentService;
        private readonly IUserService _userService;
        public BlogModelFactory(IBlogService blogService
                              , IBlogMediaService blogMediaService
                              , IBlogCommentService blogCommentService
                              , IUserService userService)
        {
            _blogService = blogService;
            _blogMediaService = blogMediaService;
            _blogCommentService = blogCommentService;
            _userService = userService;
        }

        public BlogModel PreapareBlogModel(BlogModel blogModel, Blog blog, bool showComments)
        {
            try
            {
                if (blog == null)
                {
                    throw new ArgumentNullException(nameof(blog));
                }

                if (blogModel == null)
                {
                    throw new ArgumentNullException(nameof(blogModel));
                }

                blogModel.Id = blog.Id;
                blogModel.Body = blog.Body;
                blogModel.CommentStatusId = blog.CommentStatusId;
                blogModel.CreationDate = blog.CreationDate;
                blogModel.Description = blog.Description;
                blogModel.MetaKeywords = blog.MetaKeywords;
                blogModel.ModifiedDate = blog.ModifiedDate;
                blogModel.Name = blog.Name;
                blogModel.ReadingTime = blog.ReadingTime;
                blogModel.ViewStatusId = blog.ViewStatusId;
                blogModel.VisitedCount = blog.VisitedCount;

                if (showComments)
                {
                    //get all comment for blog id
                    var blogComments = _blogCommentService.List(new BlogCommentListVM
                    {
                        BlogId = blog.Id,
                        CommentStatusId = (int)CommentStatusEnum.Open
                    });

                    if (blogComments != null)
                    {
                        foreach (var blogComment in blogComments)
                        {
                            var blogCommentModel = PrepareBlogCommentListModel(blogComment);

                            blogModel.BlogComments.Add(blogCommentModel);
                        }
                    }
                }

                //prepare blog comment model for add comment
                blogModel.BlogCommentModel = PrepareBlogCommentModel(blogModel);

                return blogModel;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public BlogCommentListModel PrepareBlogCommentListModel(BlogComment blogComment)
        {
            if (blogComment == null)
                throw new ArgumentNullException(nameof(blogComment));

            var user = _userService.GetById(blogComment.UserId);

            var blogCommentModel = new BlogCommentListModel
            {
                Id = blogComment.Id,
                BlogId = blogComment.BlogId,
                Body = blogComment.Body,
                CreationDate = blogComment.CreationDate,
                FullName = $"{user.FirstName} {user.LastName}",
                ParentId = blogComment.ParentId,
            };

            return blogCommentModel;
        }

        public BlogCommentModel PrepareBlogCommentModel(BlogModel blogModel)
        {
            var blogCommentModel = new BlogCommentModel
            {
                BlogId = blogModel.Id,
                Slug = CalculateWordsCount.CleanSeoUrl(blogModel.Name)
            };

            return blogCommentModel;
        }

        public BlogListModel PrepareBlogList(int PageIndex = 1, int PageSize = 5)
        {
            var blogs = _blogService.List(new BlogListVM { PageIndex = PageIndex - 1, PageSize = PageSize });

            var blogListModel = new BlogListModel();

            var blogsModel = blogs.Select(blog =>
              {
                  var blogModel = new BlogModel();
                  blogModel.Id = blog.Id;
                  blogModel.Body = blog.Body;
                  blogModel.CommentStatusId = blog.CommentStatusId;
                  blogModel.CreationDate = blog.CreationDate;
                  blogModel.MetaKeywords = blog.MetaKeywords;
                  blogModel.ModifiedDate = blog.ModifiedDate;
                  blogModel.Name = blog.Name;
                  blogModel.ReadingTime = blog.ReadingTime;
                  blogModel.ViewStatusId = blog.ViewStatusId;
                  blogModel.VisitedCount = blog.VisitedCount;
                  blogModel.Description = CommonHelper.GetFieldByLength(blog.Description);

                  return blogModel;
              });

            blogListModel.AvailableBlogs = blogsModel.ToList();
            blogListModel.PagingInfo = new PagingInfo { CurrentPage = PageIndex == 0 ? 1 : PageIndex, TotalItems = blogs.TotalCount, ItemsPerPage = 2, Route = "Blogs" };

            return blogListModel;
        }

    }
}
