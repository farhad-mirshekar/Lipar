using Lipar.Entities.Domain.Application;
using Lipar.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Lipar.Web.Factories.Application
{
    public class ShoppingCartItemModelFactory : IShoppingCartItemModelFactory
    {
        #region Ctor

        #endregion

        #region Fields

        #endregion

        #region Methods
        public ResultViewModel AddToCartWithAttribute(Product product, FormCollection formCollection)
        {
            throw new System.NotImplementedException();
        }

        public ResultViewModel AddToCartWithoutAttribute(Product product)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
