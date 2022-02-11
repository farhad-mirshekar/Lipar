using System;

namespace Lipar.Web.Models.Application
{
    public class ProductCommentModel : BaseEntityModel<Guid>
    {
        public string CommentText { get; set; }
        public Guid ProductId { get; set; }
        public Guid? ParentId { get; set; }
        public string Slug { get; set; }
    }
}
