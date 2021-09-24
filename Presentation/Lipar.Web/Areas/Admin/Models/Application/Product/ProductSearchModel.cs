using Nop.Web.Framework.Models;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductSearchModel : BaseSearchModel
    {
        public string Name { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
