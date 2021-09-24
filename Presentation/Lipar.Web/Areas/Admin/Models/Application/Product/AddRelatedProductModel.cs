using System.Collections;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class AddRelatedProductModel :BaseEntityModel
    {
        #region Ctor
        public AddRelatedProductModel()
        {
            SelectedProductIds = new List<int>();
        }
        #endregion

        public int ProductId { get; set; }
        public IList<int> SelectedProductIds { get; set; }
    }
}
