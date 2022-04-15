using Lipar.Core;
using Lipar.Core.ReadingTime;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Factories.Portal;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Models.Portal;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace Lipar.Web.Controllers
{
    public class BlogController : BasePublishController
    {
        private readonly IBlogModelFactory _blogModelFactory;
        private readonly IBlogService _blogService;
        private readonly IWorkContext _workContext;
        private readonly IBlogCommentService _blogCommentService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IActivityLogService _activityLogService;
        public BlogController(IBlogModelFactory blogModelFactory
                            , IBlogService blogService
                            , IWorkContext workContext
                            , IBlogCommentService blogCommentService
                            , ILocaleStringResourceService localeStringResourceService
                            , IActivityLogService activityLogService)
        {
            _blogModelFactory = blogModelFactory;
            _blogService = blogService;
            _workContext = workContext;
            _blogCommentService = blogCommentService;
            _localeStringResourceService = localeStringResourceService;
            _activityLogService = activityLogService;
        }

        #region Blogs
        public IActionResult List(int? page)
        {
            var blogSettings = _blogService.BlogSettings();

            var model = _blogModelFactory.PrepareBlogList(PageIndex: page ?? 1, PageSize: blogSettings.BlogPageSize);
            return View(model);
        }

        [Route("Blog/{Id:guid}")]
        public IActionResult Detail(Guid Id)
        {
            var blog = _blogService.GetById(Id);
            if (blog == null)
                return InvokeHttp404();

            if (blog == null || blog.ViewStatusId == (int)ViewStatusEnum.InActive)
                return InvokeHttp404();

            var model = new BlogModel();
            model = _blogModelFactory.PreapareBlogModel(model, blog, true);
            return View(model);
        }
        #endregion

        #region Blog Comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(BlogCommentModel model)
        {
            if (ModelState.IsValid)
            {
                //check comments disable for user current
                var comments = _blogCommentService.List(new BlogCommentListVM
                {
                    UserId = _workContext.CurrentUser.Id,
                    CommentStatusId = (int)CommentStatusEnum.Close,
                });

                if (comments.Any())
                    TempData["Lipar.Web.Blog.Comment"] = _localeStringResourceService.GetResource("Web.Comment.Error.UserHasADisabledComment");

                var blogComment = new BlogComment
                {
                    BlogId = model.BlogId,
                    Body = model.Body,
                    CommentStatusId = (int)CommentStatusEnum.Close,
                    UserId = _workContext.CurrentUser.Id
                };

                _blogCommentService.Add(blogComment);

                //add activity log for create blog comment
                //_activityLogService.Add("Web.Comment.Create", _localeStringResourceService.GetResource("ActivityLog.Web.Comment.Create"), blogComment);

                TempData["Lipar.Web.Blog.Comment"] = _localeStringResourceService.GetResource("Web.Comment.CreateText");
            }

            return RedirectToRoute("BlogDetail", new { EntityName = "Blog", SeName = CalculateWordsCount.CleanSeoUrl(model.Slug) });
        }
        #endregion
    }
}
