using Lipar.Entities.Domain.Portal;
using Lipar.Web.Models.Portal;

namespace Lipar.Web.Factories.Portal
{
   public interface IStaticPageModelFactory
    {
        /// <summary>
        /// prepare static page
        /// </summary>
        /// <returns></returns>
        StaticPageModel PrepareStaticPageModel(StaticPage staticPage);
    }
}
