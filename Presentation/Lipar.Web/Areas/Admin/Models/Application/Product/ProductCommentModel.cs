using Lipar.Entities.Domain.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductCommentModel : BaseEntityModel<Guid>
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
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid? ParentId { get; set; }
        public string FullName { get; set; }
        public Guid UserId { get; set; }
        public int CommentStatusId { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableCommentStatusType { get; set; }
        #endregion
    }
}
