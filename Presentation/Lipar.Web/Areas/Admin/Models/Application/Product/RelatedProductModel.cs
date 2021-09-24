namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class RelatedProductModel : BaseEntityModel
    {
        public int ProductId1 { get; set; }
        public int ProductId2 { get; set; }
        public int Priority { get; set; }
        public string ProductName2 { get; set; }
    }
}
