using Lipar.Core;
using Lipar.Web.Factories.Application;
using Lipar.Web.Framework.Controllers;
using Melli.Portal.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Controllers
{
    public class RedirectController : BaseController
    {
        #region Ctor
        public RedirectController(IRedirectModelFactory redirectModelFactory
                                , IWorkContext workContext)
        {
            _redirectModelFactory = redirectModelFactory;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IRedirectModelFactory _redirectModelFactory;
        private readonly IWorkContext _workContext;
        #endregion

        #region Methods
        public IActionResult Melli(PurchaseResult model)
        {
            var shoppingCartItemId = _workContext.ShoppingCartItems;

            if(!shoppingCartItemId.HasValue || (shoppingCartItemId.HasValue && shoppingCartItemId.Value == Guid.Empty))
            {
                return NotFoundView();
            }

            if (model is null)
            {
                return NotFoundView();
            }

           var redirectModel = _redirectModelFactory.PrepareMelli(model);

            return View(redirectModel);
        }
        #endregion
    }
}
