namespace Lipar.Web.Models.Portal
{
    public class BlogCommentModel : BaseEntityModel
    {
        public int BlogId { get; set; }
        public int? ParentId { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
    }
}
