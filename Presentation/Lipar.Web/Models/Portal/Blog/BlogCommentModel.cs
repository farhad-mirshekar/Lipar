using System;

namespace Lipar.Web.Models.Portal
{
    public class BlogCommentModel : BaseEntityModel<Guid>
    {
        public Guid BlogId { get; set; }
        public Guid? ParentId { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
    }
}
