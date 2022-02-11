using System;

namespace Lipar.Entities.Domain.Application
{
    public class ProductCommentListVM : BaseListVM
    {
        public Guid? ProductId { get; set; }
        public int? CommentStatusId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? UserId { get; set; }
    }
}
