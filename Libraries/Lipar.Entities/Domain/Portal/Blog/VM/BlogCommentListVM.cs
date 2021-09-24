namespace Lipar.Entities.Domain.Portal
{
    public class BlogCommentListVM : BaseListVM
    {
        public int? BlogId { get; set; }
        public int? ParentId { get; set; }
        public int? UserId { get; set; }
        public int? CommentStatusId { get; set; }
    }
}
