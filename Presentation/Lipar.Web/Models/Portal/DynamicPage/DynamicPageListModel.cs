using Lipar.Web.Framework.Models;
using System.Collections.Generic;

namespace Lipar.Web.Models.Portal
{
    public class DynamicPageDetailListModel
    {
        public DynamicPageDetailListModel()
        {
            AvailableDynamicPageDetailModel = new List<DynamicPageDetailModel>();
            PagingInfo = new PagingInfo();
            DynamicPageModel = new DynamicPageModel();
        }
        public DynamicPageModel DynamicPageModel { get; set; }
        public IEnumerable<DynamicPageDetailModel> AvailableDynamicPageDetailModel { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
