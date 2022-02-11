using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class BlogModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public BlogModel()
        {
            AvailableCommentStatusType = new List<SelectListItem>();
            AvailableViewStatusType = new List<SelectListItem>();
            BlogMediaSearchModel = new BlogMediaSearchModel();
            AvailableLanguageType = new List<SelectListItem>();
            AvailableCategoryType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string Description { get; set; }
        public int CommentStatusId { get; set; }
        public int VisitedCount { get; set; }
        public int ViewStatusId { get; set; }
        public Guid CategoryId { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string ReadingTime { get; set; }
        public int? LanguageId { get; set; }
        public int ApprovedComments { get; set; }
        public int NotApprovedComments { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableCommentStatusType { get; set; }
        public IList<SelectListItem> AvailableViewStatusType { get; set; }
        public IList<SelectListItem> AvailableLanguageType { get; set; }
        public IList<SelectListItem> AvailableCategoryType { get; set; }
        #endregion

        public BlogMediaSearchModel BlogMediaSearchModel { get; set; }
    }
}
