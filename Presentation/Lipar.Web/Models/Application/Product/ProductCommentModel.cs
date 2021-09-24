namespace Lipar.Web.Models.Application
{
    public class ProductCommentModel : BaseEntityModel
    {
        public string CommentText { get; set; }
        public int ProductId { get; set; }
        public int? ParentId { get; set; }
        public string Slug { get; set; }
    }
}
