using Lipar.Entities.Domain.Application;
using System;
using System.Collections.Generic;

namespace Lipar.Services.Application.Contracts
{
   public interface ICompareProductService
    {
        /// <summary>
        /// add product to compare list method
        /// </summary>
        /// <param name="ProductId">product id</param>
        void AddProductToCompareList(Guid ProductId);
        /// <summary>
        /// delete all product in compare list method
        /// </summary>
        void ClearCompareProducts();
        /// <summary>
        /// list compare product method
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetCompareProducts();
        /// <summary>
        /// delete product from compre list
        /// </summary>
        /// <param name="ProductId">product id</param>
        void RemoveProductFromCompareList(Guid ProductId);
    }
}
