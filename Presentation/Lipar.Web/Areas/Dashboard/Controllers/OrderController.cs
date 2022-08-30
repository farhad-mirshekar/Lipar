using Lipar.Web.Areas.Dashboard.Factories.Application;
using Lipar.Web.Areas.Dashboard.Models.Application;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Areas.Dashboard.Controllers
{
    /// <summary>
    /// order controller
    /// </summary>
    public class OrderController : BaseDashboardController
    {
        #region Ctor
        public OrderController(IOrderModelFactory orderModelFactory)
        {
            _orderModelFactory = orderModelFactory;
        }
        #endregion

        #region Fields
        private readonly IOrderModelFactory _orderModelFactory;
        #endregion

        #region Methods
        public IActionResult List(int page = 0)
        {
            //get all order for customer
            var orders = _orderModelFactory.PrepareOrderListModel(new OrderSearchModel
            {
                Page = page
            });

            return View(orders);
        }

        public IActionResult Invoice(Guid orderId)
        {
            if(orderId == Guid.Empty)
            {
                throw new Exception("order not found");
            }

            var searchModel = new OrderSearchModel();
            searchModel.OrderId = orderId;

            var orderModel = _orderModelFactory.PrepareInvoiceDetail(searchModel);

            var orderTrackingFlows = _orderModelFactory.GetOrderDocStates(orderId);
            ViewBag.orderTrackingFlows = orderTrackingFlows;

            return View(orderModel);
        }
        #endregion
    }
}
