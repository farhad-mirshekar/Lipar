using Lipar.Core;
using Lipar.Entities.Domain.Portal;

namespace Lipar.Services.Portal.Contracts
{
    public interface IDynamicPageService
    {
        /// <summary>
        /// add dynamic page method
        /// </summary>
        /// <param name="model"></param>
        void Add(DynamicPage model);
        /// <summary>
        /// edit dynamic page method
        /// </summary>
        /// <param name="model"></param>
        void Edit(DynamicPage model);
        /// <summary>
        /// delete dynamic page method : update remover id and remove date
        /// </summary>
        /// <param name="model"></param>
        void Delete(DynamicPage model);
        /// <summary>
        /// retrieve dynamic page by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        DynamicPage GetById(int Id);
        /// <summary>
        /// list dynamic page
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<DynamicPage> List(DynamicPageListVM listVM);
    }
}
