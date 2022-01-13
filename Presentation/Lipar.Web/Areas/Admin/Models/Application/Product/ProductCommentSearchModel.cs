using Lipar.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lipar.Web.Framework.Models;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductCommentSearchModel : BaseSearchModel
    {
        #region Ctor
        public ProductCommentSearchModel()
        {
            AvailableCommentStatusType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public int CommentStatusId { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableCommentStatusType { get; set; }
        #endregion
    }
}
