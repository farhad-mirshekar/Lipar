using Lipar.Web.Models.General;
using System;

namespace Lipar.Web.Models.Application
{
    public class RelatedProductModel : BaseEntityModel<Guid>
    {
        public RelatedProductModel()
        {
            MediaModel = new MediaModel();
        }
        public Guid ProductId2 { get; set; }
        public string ProductName2 { get; set; }
        public int Priority { get; set; }
        public MediaModel MediaModel { get; set; }
    }
}
