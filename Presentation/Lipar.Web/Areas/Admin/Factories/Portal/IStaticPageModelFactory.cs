using Lipar.Entities.Domain.Portal;
using Lipar.Web.Areas.Admin.Models.Portal;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public interface IStaticPageModelFactory
    {
        /// <summary>
        /// prepare static page model for create or update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="staticPage"></param>
        /// <returns></returns>
        StaticPageModel PrepareStaticPageModel(StaticPageModel model, StaticPage staticPage);
        /// <summary>
        /// prepare model for data table
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        StaticPageListModel PrepareStaticPageListModel(StaticPageSearchModel searchModel);
    }
}
