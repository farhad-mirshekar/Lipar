using Lipar.Core;
using Lipar.Services.Application.Contracts;
using Lipar.ViewModels.Application;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Areas.Admin.Models.Organization;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class OrderModelFactory : IOrderModelFactory
    {
        #region Ctor
        public OrderModelFactory(IOrderService orderService
                               , IOrderPaymentStatusService orderPaymentStatusService
                               , IWorkContext workContext)
        {
            _orderService = orderService;
            _orderPaymentStatusService = orderPaymentStatusService;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IOrderService _orderService;
        private readonly IOrderPaymentStatusService _orderPaymentStatusService;
        private readonly IWorkContext _workContext;
        #endregion
        public OrderViewModel PrepareInvoiceDetail(Guid orderId)
        {
            var orderModel = _orderService.GetOrderViewModel(orderId);

            return orderModel;
        }
    }
}
