using Lipar.Entities.Domain.General;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.Portal
{
    public class BlogModel : BaseEntityModel
    {
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string Description { get; set; }
        public int CommentStatusId { get; set; }
        public int VisitedCount { get; set; }
        public int ViewStatusId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string ReadingTime { get; set; }
        public int? LanguageId { get; set; }
        public Media Media { get; set; }
        public IList<BlogCommentListModel> BlogComments { get; set; } = new List<BlogCommentListModel>();
        public BlogCommentModel BlogCommentModel { get; set; } = new BlogCommentModel();
    }
}
