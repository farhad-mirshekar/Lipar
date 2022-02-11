using System;
using System.ComponentModel.DataAnnotations;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class GalleryMediaModel : BaseEntityModel<Guid>
    {
        public Guid GalleryId { get; set; }
        [UIHint("Picture")]
        public Guid MediaId { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
        public string AltAttribute { get; set; }
    }
}
