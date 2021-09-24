using Lipar.Services.Portal.Contracts;
using Lipar.Web.Factories.Portal;
using Lipar.Web.Models.Portal;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lipar.Web.Components
{
    /// <summary>
    /// show blog comment
    /// </summary>
    public class BlogCommentViewComponent : ViewComponent
    {
        #region Fields
        private readonly IBlogCommentService _blogCommentService;
        private readonly IBlogModelFactory _blogModelFactory;
        #endregion

        #region Ctor
        public BlogCommentViewComponent(IBlogCommentService blogCommentService
                                  , IBlogModelFactory blogModelFactory)
        {
            _blogCommentService = blogCommentService;
            _blogModelFactory = blogModelFactory;
        }
        #endregion

        #region Methods
        public IViewComponentResult Invoke(int Id)
        {
            var blogComments = _blogCommentService.List(new Entities.Domain.Portal.BlogCommentListVM { ParentId = Id });
            var blogCommentModels = new List<BlogCommentListModel>();

            foreach (var blogComment in blogComments)
            {
                blogCommentModels.Add(_blogModelFactory.PrepareBlogCommentListModel(blogComment));
            }

            return View(blogCommentModels);
        }
        #endregion
    }
}
