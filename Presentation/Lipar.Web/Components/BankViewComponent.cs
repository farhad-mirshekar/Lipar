using Lipar.Web.Factories.Application;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Components
{
    public class BankViewComponent : ViewComponent
    {
        #region Ctor
        public BankViewComponent(IShoppingCartItemModelFactory shoppingCartItemModelFactory)
        {
            _shoppingCartItemModelFactory = shoppingCartItemModelFactory;
        }
        #endregion

        #region Fields
        private readonly IShoppingCartItemModelFactory _shoppingCartItemModelFactory;
        #endregion

        public IViewComponentResult Invoke()
        {
            var bankPorts = _shoppingCartItemModelFactory.PrepareBankList();
            return View(bankPorts);
        }
    }
}
