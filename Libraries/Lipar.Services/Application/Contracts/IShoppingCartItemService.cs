using Lipar.Entities.Domain.Application;
using System;
using System.Linq;

namespace Lipar.Services.Application.Contracts
{
    public interface IShoppingCartItemService
    {
        /// <summary>
        /// add shopping cart item method
        /// </summary>
        /// <param name="model">shopping cart item</param>
        void Add(ShoppingCartItem model);

        /// <summary>
        /// edit shopping cart item method
        /// </summary>
        /// <param name="model">shopping cart item</param>
        void Edit(ShoppingCartItem model);

        /// <summary>
        /// delete shopping cart item method
        /// </summary>
        /// <param name="model">shopping cart item</param>
        void Delete(ShoppingCartItem model);

        /// <summary>
        /// retrieve shopping cart item by id method
        /// </summary>
        /// <param name="Id">id</param>
        /// <param name="noTracking">if param True, model retrieve no tracking</param>
        /// <returns></returns>
        ShoppingCartItem GetById(int Id, bool noTracking = false);

        /// <summary>
        /// retrieve shopping cart item method
        /// </summary>
        /// <param name="shoppingCartItemId">shopping Cart Item Id</param>
        /// <param name="productId">product id</param>
        /// <returns></returns>
        ShoppingCartItem Get(Guid shoppingCartItemId, int? productId);

        /// <summary>
        /// get count shopping cart item method
        /// </summary>
        /// <param name="shoppingCartItemId">shopping cart item id</param>
        /// <returns></returns>
        int GetCountShoppingCartItem(Guid shoppingCartItemId);

        /// <summary>
        /// get shopping cart item query
        /// </summary>
        /// <param name="ShoppingCartItemId">shopping cart item id</param>
        /// <returns></returns>
        IQueryable<ShoppingCartItem> GetShoppingCartItemQuery(Guid ShoppingCartItemId);
    }
}
