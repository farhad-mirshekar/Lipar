using Lipar.Core;
using Lipar.Entities.Domain.Application;
using System;

namespace Lipar.Services.Application.Contracts
{
   public interface IProductMediaService
    {
        /// <summary>
        /// product media list method
        /// </summary>
        /// <param name="listVM">product media list vm</param>
        /// <returns></returns>
        IPagedList<ProductMedia> List(ProductMediaListVM listVM);
        /// <summary>
        /// product media save method :add or edit method implement inside
        /// </summary>
        /// <param name="model">product media</param>
        void Save(ProductMedia model);
        /// <summary>
        /// product media delete method
        /// </summary>
        /// <param name="model">product media</param>
        void Delete(ProductMedia model);
        /// <summary>
        ///  retrieve product media by id method
        /// </summary>
        /// <param name="Id">id</param>
        /// <returns></returns>
        ProductMedia GetById(Guid Id);
        /// <summary>
        /// retrieve product media by media id method
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        ProductMedia GetByMediaId(Guid mediaId);
    }
}
