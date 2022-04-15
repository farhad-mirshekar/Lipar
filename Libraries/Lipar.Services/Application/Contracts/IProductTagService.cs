using Lipar.Core;
using Lipar.Entities.Domain.Application;
using System;
using System.Collections.Generic;

namespace Lipar.Services.Application.Contracts
{
    /// <summary>
    /// product tag service
    /// </summary>
    public interface IProductTagService
    {
        /// <summary>
        /// add product tag method
        /// </summary>
        /// <param name="model"></param>
        void Add(ProductTag model);
        /// <summary>
        /// edit product tag method
        /// </summary>
        /// <param name="model"></param>
        void Edit(ProductTag model);
        /// <summary>
        /// delete product tag method
        /// </summary>
        /// <param name="model"></param>
        void Delete(ProductTag model);
        /// <summary>
        /// retrieve product tag by id method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        ProductTag GetById(Guid id,bool noTracking = false);
        /// <summary>
        /// list product tag method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<ProductTag> List(ProductTagListVM listVM);

        /// <summary>
        /// save product tag mapping
        /// </summary>
        /// <param name="productTagIds">list of product tag id</param>
        /// <param name="productId">product id</param>
        void SaveProductTagMapping(IList<Guid> productTagIds, Guid productId);

        /// <summary>
        /// list product tag ids
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        IList<Guid> GetProductTagIds(Guid productId);

        /// <summary>
        /// list product tag
        /// </summary>
        /// <param name="productId">product id</param>
        /// <returns></returns>
        IList<ProductTagMapping> GetProductTagMappings(Guid productId);
    }
}
