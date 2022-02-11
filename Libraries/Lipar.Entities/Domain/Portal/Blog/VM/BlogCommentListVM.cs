using System;

namespace Lipar.Entities.Domain.Portal
{
    public class BlogCommentListVM : BaseListVM
    {
        public Guid? BlogId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? UserId { get; set; }
        public int? CommentStatusId { get; set; }
    }
}
