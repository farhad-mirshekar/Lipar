using Lipar.Entities.Domain.Application;
using Lipar.Web.Models;
using Lipar.Web.Models.Application;
using Lipar.Web.Models.Financial;
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

        /// <summary>
        /// prepare mini shopping cart item for checkout
        /// </summary>
        /// <param name="shoppingCartItemId">shopping cart item id</param>
        /// <returns></returns>
        MiniShoppingCartItemModel PrepareMiniShoppingCartItemModel(Guid shoppingCartItemId);

        /// <summary>
        /// prepare bank list
        /// </summary>
        /// <returns></returns>
        IList<BankModel> PrepareBankList();

        /// <summary>
        /// Payment
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="addressId"></param>
        /// <param name="shoppingCartItemId"></param>
        string Payment(Guid bankId, Guid addressId, Guid shoppingCartItemId);

        /// <summary>
        /// remove all basket
        /// </summary>
        /// <param name="shoppingCartItemsId"></param>
        void DeleteAllShoppingCartItem(Guid shoppingCartItemsId);

        /// <summary>
        /// clear cache shopping cart
        /// </summary>
        void ClearCacheShoppingCart();
    }
}
