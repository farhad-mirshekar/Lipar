using Lipar.Entities.Domain.Application;

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
    }
}
