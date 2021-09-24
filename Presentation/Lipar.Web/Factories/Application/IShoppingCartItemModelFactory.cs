using Lipar.Entities.Domain.Application;
using Lipar.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Lipar.Web.Factories.Application
{
   public interface IShoppingCartItemModelFactory
    {
        /// <summary>
        /// add to shopping cart item for product have attribute
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="formCollection">form collection</param>
        /// <returns></returns>
        ResultViewModel AddToCartWithAttribute(Product product, FormCollection formCollection);

        /// <summary>
        /// add to shopping cart item for product not have attribute
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        ResultViewModel AddToCartWithoutAttribute(Product product);
    }
}
