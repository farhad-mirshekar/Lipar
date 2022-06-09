using Lipar.Core;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.Enums;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Dashboard.Models.Application;
using Lipar.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Areas.Dashboard.Factories.Application
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

        #region Methods

        public OrderListModel PrepareOrderListModel(OrderSearchModel searchModel)
        {
            var customerId = _workContext.CurrentUser.Id;
            var totalRowCount = 0;

            var orderPaymentStatuses = _orderPaymentStatusService.GetOrderForCustomer(new OrderPaymentStatusListVM
            {
                UserId = customerId,
                PageSize = 5,
                PageIndex = searchModel.Page,
                OrderPaymentStatusTypeId = (int)OrderPaymentStatusTypeEnum.Paid
            }, out totalRowCount);

            var orders = orderPaymentStatuses.Select(item => new OrderModel
            {
                Price = item.Order.Price,
                CreationDate = item.CreationDate,
                Id = item.OrderId.Value,
                AvailableOrderDetails = item.Order.OrderDetails.Select(o => new OrderDetailModel
                {
                    ProductName = o.ProductName
                })
            });

            var orderListModel = new OrderListModel();

            orderListModel.AvailableOrders = orders.ToList();

            orderListModel.PagingInfo = new PagingInfo
            {
                CurrentPage = searchModel.Page == 0 ? 1 : searchModel.Page,
                TotalItems = totalRowCount,
                ItemsPerPage = 5,
            };

            return orderListModel;
        }

        public OrderModel PrepareInvoiceDetail(OrderSearchModel searchModel)
        {
            var orderQuery = _orderService.GetQuery();

            if (searchModel.OrderId.HasValue && searchModel.OrderId.Value != Guid.Empty)
            {
                orderQuery = orderQuery.Where(o => o.Id == searchModel.OrderId);
            }

            var orderDetail = orderQuery.Select(order => new OrderModel
            {
                Id = order.Id,
                CreationDate = order.CreationDate,
                Price = order.Price,
                BankName = order.BankPort.Bank.Name,
                CustomerInfo = new Models.Organization.ProfileModel
                {
                    FirstName = order.User.FirstName,
                    LastName = order.User.LastName,
                    CellPhone = order.User.CellPhone,
                },
                AvailableOrderDetails = order.OrderDetails.Select(d => new OrderDetailModel
                {
                    ProductName = d.ProductName,
                    Quantity = d.Quantity,
                    DeliveryDateName = d.DeliveryDateName,
                    ProductDiscountPrice = d.ProductDiscountPrice,
                    ProductPrice = d.ProductPrice,
                    ShippingCostName = d.ShippingCostName,
                    ProductDiscountTypeId = d.ProductDiscountTypeId,
                })
            }).FirstOrDefault();

            return orderDetail;
        }

        #endregion
    }
}
