using Lipar.Web.Models.General;

namespace Lipar.Web.Models.Application
{
    public class RelatedProductModel : BaseEntityModel
    {
        public RelatedProductModel()
        {
            MediaModel = new MediaModel();
        }
        public int ProductId2 { get; set; }
        public string ProductName2 { get; set; }
        public int Priority { get; set; }
        public MediaModel MediaModel { get; set; }
    }
}
