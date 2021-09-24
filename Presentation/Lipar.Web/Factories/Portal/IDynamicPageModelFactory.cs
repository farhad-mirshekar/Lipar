using Lipar.Entities.Domain.Portal;
using Lipar.Web.Models.Portal;

namespace Lipar.Web.Factories.Portal
{
   public interface IDynamicPageModelFactory
    {
        /// <summary>
        /// prepare dynamic page detail list by dynamic page
        /// </summary>
        /// <param name="dynamicPage"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        DynamicPageDetailListModel PrepareDynamicPageDetailListModel(DynamicPage dynamicPage ,int PageIndex = 1, int PageSize = int.MaxValue);
        /// <summary>
        /// prepare dynamic page detail
        /// </summary>
        /// <param name="dynamicPageDetail">id pass generic path route</param>
        /// <returns></returns>
        DynamicPageDetailModel PrepareDynamicPageDetailModel(DynamicPageDetail dynamicPageDetail);
    }
}
