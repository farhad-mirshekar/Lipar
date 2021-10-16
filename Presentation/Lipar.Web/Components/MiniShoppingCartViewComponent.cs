using Lipar.Core;
using Lipar.Services.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Components
{
    public class MiniShoppingCartViewComponent : ViewComponent
    {
        #region Ctor
        public MiniShoppingCartViewComponent(IShoppingCartItemService shoppingCartItemService
                                       , IWorkContext workContext)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Methods
        public IViewComponentResult Invoke()
        {
            var countShoppingCartItem = 0;
            if (_workContext.ShoppingCartItems.HasValue && _workContext.ShoppingCartItems.Value != Guid.Empty)
            {
                countShoppingCartItem = _shoppingCartItemService.GetCountShoppingCartItem(_workContext.ShoppingCartItems.Value);
            }
            return View(countShoppingCartItem);
        }
        #endregion
    }
}
