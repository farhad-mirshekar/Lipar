using System;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class RelatedProductModel : BaseEntityModel<Guid>
    {
        public Guid ProductId1 { get; set; }
        public Guid ProductId2 { get; set; }
        public int Priority { get; set; }
        public string ProductName2 { get; set; }
    }
}
