using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Entities.Domain.Portal
{
    /// <summary>
    /// gallery_media_mapping
    /// </summary>
    public class GalleryMedia : BaseEntity<Guid>
    {
        public Guid GalleryId { get; set; }
        public Guid MediaId { get; set; }
        public int Priority { get; set; }

        public Gallery Gallery { get; set; }
        public Media Media { get; set; }
    }
}
