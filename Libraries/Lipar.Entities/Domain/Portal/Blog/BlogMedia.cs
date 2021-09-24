namespace Lipar.Entities.Domain.Portal
{
    /// <summary>
    /// blog_media_mapping
    /// </summary>
    public class BlogMedia : BaseEntity
    {
        public int BlogId { get; set; }
        public int MediaId { get; set; }
        public int Priority { get; set; }
    }
}
