namespace Lipar.Entities.Domain.Portal
{
    /// <summary>
    /// gallery_media_mapping
    /// </summary>
    public class GalleryMedia : BaseEntity
    {
        public int GalleryId { get; set; }
        public int MediaId { get; set; }
        public int Priority { get; set; }
    }
}
