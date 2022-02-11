using System;

namespace Lipar.Web.Models.General
{
    public class MediaModel : BaseEntityModel<Guid>
    {
        public int Priority { get; set; }
        public string Name { get; set; }
        public string AltAttribute { get; set; }
        public string MimeType { get; set; }
        public Guid MediaId { get; set; }
    }
}
