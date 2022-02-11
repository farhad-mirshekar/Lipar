using System;

namespace Lipar.Entities.Domain.Portal
{
    public class GalleryMediaListVM : BaseListVM
    {
        public Guid? GalleryId { get; set; }
        public Guid? MediaId { get; set; }
    }
}
