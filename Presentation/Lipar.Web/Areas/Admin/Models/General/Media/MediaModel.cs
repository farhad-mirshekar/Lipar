using System.ComponentModel.DataAnnotations;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class MediaModel : BaseEntityModel
    {

        [UIHint("Picture")]
        public int MediaId { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
        public string AltAttribute { get; set; }
    }
}
