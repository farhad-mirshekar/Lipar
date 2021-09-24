using Lipar.Entities.Domain.Portal;
using Lipar.Entities.Domain.General;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Factories.Portal;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Framework.MVC;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class BlogController : BaseAdminController
    {
        #region Fields
        private readonly IBlogModelFactory _blogModelFactory;
        private readonly IBlogService _blogService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IMediaService _mediaService;
        private readonly ICommandService _commandService;
        private readonly IBlogMediaService _blogMediaService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IBlogCommentService _blogCommentService;
        private readonly INotificationService _notificationService;
        #endregion

        #region Ctor
        public BlogController(IBlogModelFactory blogModelFactory
                            , IBlogService blogService
                            , IUrlRecordService urlRecordService
                            , IMediaService mediaService
                            , ICommandService commandService
                            , IBlogMediaService blogMediaService
                            , IActivityLogService activityLogService
                            , ILocaleStringResourceService localeStringResourceService
                            , IBlogCommentService blogCommentService
                            , INotificationService notificationService)
        {
            _blogModelFactory = blogModelFactory;
            _blogService = blogService;
            _urlRecordService = urlRecordService;
            _mediaService = mediaService;
            _commandService = commandService;
            _blogMediaService = blogMediaService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _blogCommentService = blogCommentService;
            _notificationService = notificationService;
        }
        #endregion

        #region Methods
        public IActionResult Index
            => RedirectToAction("List");

        public IActionResult List()
            => View(new BlogSearchModel());

        [HttpPost]
        public IActionResult List(BlogSearchModel searchModel)
        {
            if (!_commandService.CheckPermission("ManageBlog"))
                return AccessDeniedView();

            var model = _blogModelFactory.PrepareBlogListModel(searchModel);

            return Json(model);
        }

        public IActionResult Create()
        {
            if (!_commandService.CheckPermission("ManageBlog"))
                return AccessDeniedView();

            var model = _blogModelFactory.PrepareBlogModel(new BlogModel(), null);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(BlogModel model)
        {
            if (!_commandService.CheckPermission("ManageBlog"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var blog = model.ToEntity<Blog>();

                _blogService.Add(blog);

                // url record add 
                _urlRecordService.SaveSlug(blog, blog.Name, blog.LanguageId.Value);

                // add activity log for create blog
                _activityLogService.Add("Admin.blog.Create", _localeStringResourceService.GetResource("ActivityLog.Admin.blog.Create"), blog);

                //success add
                return RedirectToAction("Edit", new { Id = blog.Id });
            }

            model = _blogModelFactory.PrepareBlogModel(model, null);
            return View(model);
        }
        public IActionResult Edit(int Id)
        {
            if (!_commandService.CheckPermission("ManageBlog"))
                return AccessDeniedView();

            var blog = _blogService.GetById(Id);
            if (blog == null)
                return RedirectToAction("List");

            var model = _blogModelFactory.PrepareBlogModel(null, blog);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(BlogModel model)
        {
            if (!_commandService.CheckPermission("ManageBlog"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var blog = model.ToEntity<Blog>();

                _blogService.Edit(blog);

                // url record add 
                _urlRecordService.SaveSlug(blog, blog.Name, blog.LanguageId.Value);

                // add activity log for edit blog
                _activityLogService.Add("Admin.blog.Edit", _localeStringResourceService.GetResource("ActivityLog.Admin.blog.Edit"), blog);

                //_notificationService.SusscessNotification("تغییرات با موفقیت ایجاد شد");

                //success add
                return RedirectToAction("Edit", new { Id = blog.Id });
            }

            model = _blogModelFactory.PrepareBlogModel(model, null);
            return View(model);
        }
        #endregion

        #region Blog-Media

        [HttpPost]
        public IActionResult BlogMediaList(BlogMediaSearchModel searchModel)
        {
            var model = _blogModelFactory.PrepareBlogMediaListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public IActionResult BlogMediaEdit(BlogMediaModel model)
        {
            //var blogMediaResult = await _blogService.GetPictureById(model.Id);
            //if (!blogMediaResult.Success)
            //    return new NullJsonResult();

            //var blogMedia = blogMediaResult.Data;

            //await _mediaService.EditPictureAsync(new Media
            //{
            //    Id = blogMedia.MediaId,
            //    AltAttribute = model.AltAttribute,
            //    Name = model.Name,
            //});


            //await _blogService.SavePicturesAsync(blogMedia);

            return new NullJsonResult();
        }

        [HttpPost]
        public IActionResult BlogMediaAdd(BlogMediaModel model)
        {
            _mediaService.EditPicture(new Media
            {
                Id = model.MediaId,
                AltAttribute = model.AltAttribute,
                Name = model.Name
            });


            _blogMediaService.Save(new BlogMedia { BlogId = model.BlogId, MediaId = model.MediaId });

            return Json(new { Result = true });
        }

        [HttpPost]
        public IActionResult BlogMediaDelete(int Id)
        {
            //Id : MediaId

            //_mediaService.Delete(Id);
            //_blogService.DeletePicture(Id);

            return new NullJsonResult();
        }

        #endregion

        #region Blog-Comment
        public IActionResult BlogComments(int? filterByBlogId)
        {
            if (!_commandService.CheckPermission("ManageBlog"))
                return AccessDeniedView();

            var blog = _blogService.GetById(filterByBlogId ?? 0);

            if (blog == null && filterByBlogId.HasValue)
                return RedirectToAction("BlogComments");

            //prepare model
            var model = _blogModelFactory.PrepareBlogCommentSearchModel(new BlogCommentSearchModel(), blog);

            return View(model);
        }

        [HttpPost]
        public IActionResult Comments(BlogCommentSearchModel searchModel)
        {
            if (!_commandService.CheckPermission("ManageBlog"))
                return AccessDeniedView();

            var model = _blogModelFactory.PrepareBlogCommentListModel(searchModel);
            return Json(model);
        }

        public IActionResult BlogCommentEdit(int Id)
        {
            if (!_commandService.CheckPermission("ManageBlog"))
                return AccessDeniedView();

            var blogComment = _blogCommentService.GetById(Id);
            if (blogComment == null)
                return RedirectToAction("BlogComments");

            var model = _blogModelFactory.PrepareBlogCommentModel(null, blogComment);
            return View(model);
        }

        [HttpPost]
        public IActionResult BlogCommentEdit(BlogCommentModel model)
        {
            if (!_commandService.CheckPermission("ManageBlog"))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var blogComment = _blogCommentService.GetById(model.Id);

                blogComment.CommentStatusId = model.CommentStatusId;
                blogComment.Body = model.Body;

                _blogCommentService.Edit(blogComment);

                return RedirectToAction("BlogComments");
            }

            model = _blogModelFactory.PrepareBlogCommentModel(model, null);
            return View(model);
        }

        [HttpPost]
        public IActionResult BlogCommentDelete(int Id)
        {
            if (Id == 0)
                return RedirectToAction("BlogComments");

            var blogComment = _blogCommentService.GetById(Id);
            
            if (blogComment == null)
                return RedirectToAction("BlogComments");

            _blogCommentService.Delete(blogComment);

            return new NullJsonResult();
        }
        #endregion
    }
}
