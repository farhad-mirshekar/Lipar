using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Portal;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    public class Media : BaseEntity<Guid>
    {
        public Media()
        {
            ProductMedias = new HashSet<ProductMedia>();
            BlogMedias = new HashSet<BlogMedia>();
            GalleryMedias = new HashSet<GalleryMedia>();
            NewsMedias = new HashSet<NewsMedia>();
        }
        public string Name { get; set; }
        public string AltAttribute { get; set; }
        public string MimeType { get; set; }

        public ICollection<ProductMedia> ProductMedias { get; set; }
        public ICollection<BlogMedia> BlogMedias { get; set; }
        public ICollection<GalleryMedia> GalleryMedias { get; set; }
        public ICollection<NewsMedia> NewsMedias { get; set; }
    }
}
