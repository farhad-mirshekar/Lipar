using Lipar.Core;
using Lipar.Entities.Domain.Portal;

namespace Lipar.Services.Portal.Contracts
{
   public interface IStaticPageService
    {
        /// <summary>
        /// add static page method
        /// </summary>
        /// <param name="model"></param>
        void Add(StaticPage model);
        /// <summary>
        /// edit static page method
        /// </summary>
        /// <param name="model"></param>
        void Edit(StaticPage model);
        /// <summary>
        /// delete static page method : update remover id and remove date
        /// </summary>
        /// <param name="model"></param>
        void Delete(StaticPage model);
        /// <summary>
        /// retrieve static page by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        StaticPage GetById(int Id);
        /// <summary>
        /// list static page
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<StaticPage> List(StaticPageListVM listVM);
    }
}
