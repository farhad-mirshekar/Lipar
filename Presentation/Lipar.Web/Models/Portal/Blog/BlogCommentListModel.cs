using System.Collections.Generic;

namespace Lipar.Web.Models.Portal
{
    public class BlogCommentListModel : BaseEntityModel
    {
        public int BlogId { get; set; }
        public string Body { get; set; }
        public string FullName { get; set; }
        public IList<BlogCommentListModel> Children { get; set; } = new List<BlogCommentListModel>();
        public int? ParentId { get; set; }
    }
}
