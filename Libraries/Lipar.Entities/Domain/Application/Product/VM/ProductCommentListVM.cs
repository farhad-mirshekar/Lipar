namespace Lipar.Entities.Domain.Application
{
    public class ProductCommentListVM : BaseListVM
    {
        public int? ProductId { get; set; }
        public int? CommentStatusId { get; set; }
        public int? ParentId { get; set; }
        public int? UserId { get; set; }
    }
}
