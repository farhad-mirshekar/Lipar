using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.Enums;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Application.Contracts;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class OrderTrackingFlowModelFactory : IOrderTrackingFlowModelFactory
    {
        #region Ctor
        public OrderTrackingFlowModelFactory(IOrderTrackingFlowService orderTrackingFlowService
                                           , IWorkContext workContext
                                           , IPositionService positionService)
        {
            _orderTrackingFlowService = orderTrackingFlowService;
            _workContext = workContext;
            _positionService = positionService;
        }
        #endregion

        #region Fields
        private readonly IOrderTrackingFlowService _orderTrackingFlowService;
        private readonly IWorkContext _workContext;
        private readonly IPositionService _positionService;
        #endregion

        #region Methods

        public OrderTrackingFlowListModel PreapreOrderTrackingFlowListModel(OrderTrackingFlowSearchModel searchModel)
        {
            var orderTrackingFlowQuery = _orderTrackingFlowService.GetOrderTrackingFlows(new OrderTrackingFlowListVM
            {
                ToPositionId = searchModel.ToPositionId,
                PageSize = searchModel.PageSize,
                PageIndex = searchModel.Page - 1,
                ActionState = true
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
                                                  Id = otf.Id,
                                                  OrderTrackingId = otf.OrderTrackingId,
                                                  OrderId = otf.OrderTracking.OrderId,
                                                  BankName = otf.OrderTracking.Order.BankPort.Bank.Name,
                                                  CustomerFullName = otf.OrderTracking.Order.User.FirstName + " " + otf.OrderTracking.Order.User.LastName,
                                                  PaymentDate = otf.OrderTracking.Order.CreationDate,
                                                  Price = otf.OrderTracking.Order.Price,
                                              }).FirstOrDefault();

            return orderTrackingFlowModel;
        }

        public bool FinancialStep(Guid orderTrackingFlowId, Guid orderTrackingId , string description)
        {
            var result = false;
            try
            {
                if (orderTrackingFlowId == Guid.Empty)
                {
                    throw new Exception("order tracking flow id is null");
                }

                if (orderTrackingId == Guid.Empty)
                {
                    throw new Exception("order tracking id is null");
                }

                var positionType = (int)PositionTypeEnum.Manager;
                var enableType = (int)EnabledTypeEnum.Active;
                var departmentType = (int)DepartmentTypeEnum.WarehouseUnit;
                var positionId = _workContext.CurrentPosition.Id;

                var currentFlow = _orderTrackingFlowService.GetById(orderTrackingFlowId,
                                                                    positionId);
                if (currentFlow is null)
                {
                    throw new Exception("flow not found");
                }

                currentFlow.ActionDate = CommonHelper.GetCurrentDateTime();

                var warehousePosition = _positionService.GetPosition(positionType, enableType, departmentType);

                if (warehousePosition is null)
                {
                    throw new Exception("warehouse Position not found");
                }

                var flow = new OrderTrackingFlow();
                flow.OrderTrackingId = orderTrackingId;
                flow.FromPositionId = currentFlow.ToPositionId;
                flow.FromDocStateId = currentFlow.ToDocStateId;
                flow.ToDocStateId = (int)OrderTrackingDocStateTypeEnum.WarehouseUnitReview;
                flow.ToPositionId = warehousePosition.Id;
                flow.Description = description;

                _orderTrackingFlowService.Edit(currentFlow);
                _orderTrackingFlowService.Add(flow);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool WarehouseStep(Guid orderTrackingFlowId, Guid orderTrackingId, string description)
        {
            var result = false;
            try
            {
                if (orderTrackingFlowId == Guid.Empty)
                {
                    throw new Exception("order tracking flow id is null");
                }

                if (orderTrackingId == Guid.Empty)
                {
                    throw new Exception("order tracking id is null");
                }
                var positionId = _workContext.CurrentPosition.Id;

                var currentFlow = _orderTrackingFlowService.GetById(orderTrackingFlowId,
                                                                    positionId);
                if (currentFlow is null)
                {
                    throw new Exception("flow not found");
                }

                currentFlow.ActionDate = CommonHelper.GetCurrentDateTime();

                var flow = new OrderTrackingFlow();
                flow.OrderTrackingId = orderTrackingId;
                flow.FromPositionId = currentFlow.ToPositionId;
                flow.FromDocStateId = currentFlow.ToDocStateId;
                flow.ToDocStateId = (int)OrderTrackingDocStateTypeEnum.SendProduct;
                flow.Description = description;

                _orderTrackingFlowService.Edit(currentFlow);
                _orderTrackingFlowService.Add(flow);
            }
            catch(Exception ex)
            {
                result = false;
            }

            return result;
        }

        #endregion

        #region Utilities

        #endregion
    }
}
