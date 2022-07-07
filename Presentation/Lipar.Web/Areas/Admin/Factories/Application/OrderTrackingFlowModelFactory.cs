using Lipar.Core;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class OrderTrackingFlowModelFactory : IOrderTrackingFlowModelFactory
    {
        #region Ctor
        public OrderTrackingFlowModelFactory(IOrderTrackingFlowService orderTrackingFlowService
                                           , IWorkContext workContext)
        {
            _orderTrackingFlowService = orderTrackingFlowService;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IOrderTrackingFlowService _orderTrackingFlowService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Methods

        public OrderTrackingFlowListModel PreapreOrderTrackingFlowListModel(OrderTrackingFlowSearchModel searchModel)
        {
            var orderTrackingFlowQuery = _orderTrackingFlowService.GetOrderTrackingFlows(new OrderTrackingFlowListVM
            {
                ToPositionId = searchModel.ToPositionId,
                PageSize = searchModel.PageSize,
                PageIndex = searchModel.Page - 1
            });

            var orderTrackingFlowModel = orderTrackingFlowQuery.Select(o => new OrderTrackingFlowModel
            {
                BankName = o.BankName,
                CustomerFullName = o.CustomerFullName,
                Id = o.Id,
                OrderId = o.OrderId,
                OrderTrackingId = o.OrderTrackingId,
                PaymentDate = o.PaymentDate,
                Price = o.Price,
            });

            var orderTrackingFlowModels = new PagedList<OrderTrackingFlowModel>(orderTrackingFlowModel,
                                                                                searchModel.Page - 1,
                                                                                searchModel.PageSize);

            var models = new OrderTrackingFlowListModel().PrepareToGrid(searchModel, orderTrackingFlowModels, () =>
            {
                return orderTrackingFlowModels.Select(otf =>
                {
                    return otf;
                });
            });

            return models;
        }

        public OrderTrackingFlowSearchModel PrepareOrderTrackingFlowSearchModel()
        {
            var searchModel = new OrderTrackingFlowSearchModel();
            searchModel.ToPositionId = _workContext.CurrentPosition.Id;

            return searchModel;
        }

        public OrderTrackingFlowModel PrepareOrderTrackingFlowModel(OrderTrackingFlowSearchModel searchModel)
        {
            var query = _orderTrackingFlowService.GetQuery();

            var orderTrackingFlowModel = query.Where(otf => otf.OrderTrackingId == searchModel.OrderTrackingId.Value
                                                         && otf.ToPositionId == _workContext.CurrentPosition.Id)
                                              .Select(otf => new OrderTrackingFlowModel
                                              {
                                                  OrderTrackingId = otf.OrderTrackingId,
                                                  OrderId = otf.OrderTracking.OrderId,
                                                  BankName = otf.OrderTracking.Order.BankPort.Bank.Name,
                                                  CustomerFullName = otf.OrderTracking.Order.User.FirstName + " " + otf.OrderTracking.Order.User.LastName,
                                                  PaymentDate = otf.OrderTracking.Order.CreationDate,
                                                  Price = otf.OrderTracking.Order.Price,
                                              }).FirstOrDefault();

            return orderTrackingFlowModel;
        }

        #endregion
    }
}
