using Lipar.Entities.Domain.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductCommentModel : BaseEntityModel
    {
        #region Ctor
        public ProductCommentModel()
        {
            AvailableCommentStatusType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string CommentText { get; set; }
        public string ReplyText { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ParentId { get; set; }
        public string FullName { get; set; }
        public int UserId { get; set; }
        public int CommentStatusId { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableCommentStatusType { get; set; }
        #endregion
    }
}
