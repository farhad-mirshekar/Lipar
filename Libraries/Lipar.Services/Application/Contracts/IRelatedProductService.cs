using Lipar.Core;
using Lipar.Entities.Domain.Application;
using System;
using System.Collections.Generic;

namespace Lipar.Services.Application.Contracts
{
   public interface IRelatedProductService
    {
        /// <summary>
        /// add related product method
        /// </summary>
        /// <param name="model">related product</param>
        void Add(RelatedProduct model);
        /// <summary>
        /// edit related product method
        /// </summary>
        /// <param name="model">related product</param>
        void Edit(RelatedProduct model);
        /// <summary>
        /// delete related product method
        /// </summary>
        /// <param name="model">related product</param>
        void Delete(RelatedProduct model);
        /// <summary>
        /// retrieve related product by id method
        /// </summary>
        /// <param name="Id">id</param>
        /// <returns></returns>
        RelatedProduct GetById(Guid Id);
        /// <summary>
        /// list related product method
        /// </summary>
        /// <param name="listVM">related product list</param>
        /// <returns></returns>
        IPagedList<RelatedProduct> List(RelatedProductListVM listVM);
        /// <summary>
        /// get related product by product id 1
        /// </summary>
        /// <param name="ProductId1">product id 1</param>
        /// <returns></returns>
        IEnumerable<RelatedProduct> GetRelatedProductsByProductId1(Guid productId1);
        /// <summary>
        /// find related product
        /// </summary>
        /// <param name="source">list existing related product</param>
        /// <param name="productId1">product 1</param>
        /// <param name="productId2">product 2</param>
        /// <returns></returns>
        RelatedProduct FindRelatedProduct(IEnumerable<RelatedProduct> source, Guid productId1, Guid productId2);
    }
}
