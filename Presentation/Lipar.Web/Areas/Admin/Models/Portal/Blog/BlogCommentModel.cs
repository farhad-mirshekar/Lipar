using Lipar.Entities.Domain.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class BlogCommentModel : BaseEntityModel
    {
        #region Ctor
        public BlogCommentModel()
        {
            AvailableCommentStatusType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Body { get; set; }
        public int BlogId { get; set; }
        public int? ParentId { get; set; }
        public int CommentStatusId { get; set; }
        public string CreatorFullName { get; set; }  
        #endregion

        #region Select
        public IList<SelectListItem> AvailableCommentStatusType { get; set; }
        #endregion

        #region Navigations
        public CommentStatus CommentStatus { get; set; }
        #endregion
    }
}
