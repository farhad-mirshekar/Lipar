using Lipar.Core;
using Lipar.Entities;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Portal;
using System;

namespace Lipar.Services.Portal.Contracts
{
   public interface IDynamicPageDetailService
    {
        /// <summary>
        /// add dynamic page detail method
        /// </summary>
        /// <param name="model"></param>
        void Add(DynamicPageDetail model);
        /// <summary>
        /// edit dynamic page detail method
        /// </summary>
        /// <param name="model"></param>
        void Edit(DynamicPageDetail model);
        /// <summary>
        /// delete dynamic page detail method : update remover id and remove date
        /// </summary>
        /// <param name="model"></param>
        void Delete(DynamicPageDetail model);
        /// <summary>
        /// retrieve dynamic page detail by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        DynamicPageDetail GetById(Guid Id);
        /// <summary>
        /// list dynamic page detail
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<DynamicPageDetail> List(DynamicPageDetailListVM listVM);
        /// <summary>
        /// get dynamic page detail count by page id
        /// </summary>
        /// <param name="DynamicPageId">dynamic page id</param>
        /// <param name="viewStatus">view status enum</param>
        /// <returns></returns>
        int GetDynamicPageDetailCount(Guid DynamicPageId, ViewStatusEnum viewStatus);
    }
}
