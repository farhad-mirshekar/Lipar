using Lipar.Entities.Domain.Application;
using Lipar.Web.Models;
using Lipar.Web.Models.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Factories.Application
{
   public interface IShoppingCartItemModelFactory
    {
        /// <summary>
        /// add to shopping cart item for product have attribute
        /// </summary>
        /// <param name="productModel">product model</param>
        /// <param name="formCollection">form collection</param>
        /// <returns></returns>
        ResultViewModel AddToCartWithAttribute(ProductModel model, IFormCollection formCollection);

        /// <summary>
        /// add to shopping cart item for product not have attribute
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        ResultViewModel AddToCartWithoutAttribute(Product product);
        
        /// <summary>
        ///prepare shopping cart item model 
        /// </summary>
        /// <param name="shoppingCartItemId">shopping cart item id</param>
        /// <param name="UserId">user id</param>
        /// <returns></returns>
        ShoppingCartItemListModel PrepareShoppingCartItemListModel(Guid shoppingCartItemId);
    }
}
