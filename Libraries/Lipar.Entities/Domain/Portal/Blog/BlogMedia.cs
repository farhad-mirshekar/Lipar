using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Entities.Domain.Portal
{
    /// <summary>
    /// blog_media_mapping
    /// </summary>
    public class BlogMedia : BaseEntity<Guid>
    {
        public Guid BlogId { get; set; }
        public Guid MediaId { get; set; }
        public int Priority { get; set; }

        public Blog Blog { get; set; }
        public Media Media { get; set; }
    }
}
