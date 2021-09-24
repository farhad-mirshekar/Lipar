using System.Collections;
using System.Collections.Generic;

namespace Lipar.Web.Models.Application
{
    public class ProductCommentListModel : BaseEntityModel
    {
        public ProductCommentListModel()
        {
            Children = new List<ProductCommentListModel>();
        }
        public int ProductId { get; set; }
        public string CommentText { get; set; }
        public string ReplyText { get; set; }
        public string FullName { get; set; }
        public int? ParentId { get; set; }
        public IList<ProductCommentListModel> Children { get; set; }
    }
}
