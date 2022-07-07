using Lipar.Web.Areas.Admin.Factories.Application;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Areas.Admin.Components
{
    public class OrderViewComponent : ViewComponent
    {
        #region Ctor
        public OrderViewComponent(IOrderModelFactory orderModelFactory)
        {
            _orderModelFactory = orderModelFactory;
        }
        #endregion

        #region Fields
        private readonly IOrderModelFactory _orderModelFactory;
        #endregion

        public IViewComponentResult Invoke(Guid orderId)
        {
            var orderViewModel = _orderModelFactory.PrepareInvoiceDetail(orderId);

            return View(orderViewModel);
        }
    }
}
