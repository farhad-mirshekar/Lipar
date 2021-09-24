using System.ComponentModel.DataAnnotations;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductMediaModel : BaseEntityModel
    {
        public int ProductId { get; set; }
        [UIHint("Picture")]
        public int MediaId { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
        public string AltAttribute { get; set; }
    }
}
