using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class AddRelatedProductModel :BaseEntityModel<Guid>
    {
        #region Ctor
        public AddRelatedProductModel()
        {
            SelectedProductIds = new List<Guid>();
        }
        #endregion

        public Guid ProductId { get; set; }
        public IList<Guid> SelectedProductIds { get; set; }
    }
}
