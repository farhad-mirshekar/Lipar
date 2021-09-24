using Lipar.Entities.Domain.Portal;
using Lipar.Web.Areas.Admin.Models.Portal;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public interface IDynamicPageModelFactory
    {
        /// <summary>
        /// prepare dynamic page model for create or update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dynamicPage"></param>
        /// <returns></returns>
        DynamicPageModel PrepareDynamicPageModel(DynamicPageModel model, DynamicPage dynamicPage);
        /// <summary>
        /// prepare dynamic page list
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        DynamicPageListModel PrepareDynamicPageListModel(DynamicPageSearchModel searchModel);
        /// <summary>
        /// prepare dynamic page detail model for create or update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dynamicPageDetail"></param>
        /// <returns></returns>
        DynamicPageDetailModel PrepareDynamicPageDetailModel(DynamicPageDetailModel model, DynamicPageDetail dynamicPageDetail);
        /// <summary>
        /// prepare dynamic page detail list
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        DynamicPageDetailListModel PrepareDynamicPageDetailListModel(DynamicPageDetailSearchModel searchModel);
        /// <summary>
        /// prepare dynamic page detail search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dynamicPageDetail"></param>
        /// <returns></returns>
        DynamicPageDetailSearchModel PrepareDynamicPageDetailSearchModel(DynamicPageDetailSearchModel searchModel, DynamicPage dynamicPage);
    }
}
