using Lipar.Core;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Application.Contracts
{
   public interface IProductService
    {
        /// <summary>
        /// add product method
        /// </summary>
        /// <param name="model">product</param>
        void Add(Product model);
        /// <summary>
        /// edit product method
        /// </summary>
        /// <param name="model">product</param>
        void Edit(Product model);
        /// <summary>
        /// delete product method
        /// </summary>
        /// <param name="model">product</param>
        void Delete(Product model);
        /// <summary>
        /// retrieve product by id method
        /// </summary>
        /// <param name="Id">product id</param>
        /// <param name="noTracking">if param True, model retrieve no tracking</param>
        /// <returns></returns>
        Product GetById(Guid Id , bool noTracking = false);
        /// <summary>
        /// list product method
        /// </summary>
        /// <param name="listVM">product list vm</param>
        /// <returns></returns>
        IPagedList<Product> List(ProductListVM listVM);
        /// <summary>
        /// retrieve products by ids methods
        /// </summary>
        /// <param name="Ids">array id</param>
        /// <returns></returns>
        IEnumerable<Product> GetByIds(Guid[] Ids);
        
        /// <summary>
        /// only get name product
        /// </summary>
        /// <param name="Id">product id</param>
        /// <returns></returns>
        string GetProductName(Guid Id);

        IQueryable<ProductDTO> GetProductDTOs(ProductListVM listVM);
    }
}
