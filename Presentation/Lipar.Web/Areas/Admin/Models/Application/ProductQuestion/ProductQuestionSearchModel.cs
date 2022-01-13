using Microsoft.AspNetCore.Mvc.Rendering;
using Lipar.Web.Framework.Models;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductQuestionSearchModel : BaseSearchModel
    {
        public ProductQuestionSearchModel()
        {
            AvailableViewStatus = new List<SelectListItem>();
        }
        public int? ProductId { get; set; }
        public int? ViewStatusId { get; set; }

        #region Select
        public IList<SelectListItem> AvailableViewStatus { get; set; }
        #endregion
    }
}
