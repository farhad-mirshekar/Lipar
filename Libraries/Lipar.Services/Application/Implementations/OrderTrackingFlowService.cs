using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.DTOs;
using Lipar.Entities.Domain.Application.Enums;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Application.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class OrderTrackingFlowService : IOrderTrackingFlowService
    {
        #region Ctor
        public OrderTrackingFlowService(IRepository<OrderTrackingFlow> repository
                                      , IRepository<OrderTracking> orderTrackingRepository
                                      , IRepository<Position> positionRepository)
        {
            _repository = repository;
            _orderTrackingRepository = orderTrackingRepository;
            _positionRepository = positionRepository;
        }
        #endregion

        #region Fields
        private readonly IRepository<OrderTrackingFlow> _repository;
        private readonly IRepository<OrderTracking> _orderTrackingRepository;
        private readonly IRepository<Position> _positionRepository;
        #endregion

        #region Methods
        public void InitialRegistration(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new Exception("order is not found");
            }

            var orderTracking = new OrderTracking();
            orderTracking.Id = Guid.NewGuid();
            orderTracking.OrderId = orderId;
            orderTracking.CreationDate = CommonHelper.GetCurrentDateTime();

            _orderTrackingRepository.Insert(orderTracking);

            var firstStepFlow = FirstStepFlow(orderTracking.Id);

            _repository.Insert(firstStepFlow);

            //find manager financial
            var financialPosition = _positionRepository.TableNoTracking
                                                       .Where(p => p.Department.DepartmentTypeId == (int)DepartmentTypeEnum.FinancialUnit
                                                                && p.EnabledTypeId == (int)EnabledTypeEnum.Active
                                                                && p.PositionTypeId == (int)PositionTypeEnum.Manager)
                                                       .Select(p => p.Id)
                                                       .FirstOrDefault();

            var secondStepFlow = SecondStepFlow(orderTracking.Id, financialPosition);

            _repository.Insert(secondStepFlow);
        }

        public IQueryable<OrderTrackingFlowDTO> GetOrderTrackingFlows(OrderTrackingFlowListVM listVM)
        {
            var query = _repository.TableNoTracking;

            query = query.Where(o => o.ToPositionId == listVM.ToPositionId);

            query = query.Where(o => !o.ActionDate.HasValue);

            query = query.OrderByDescending(o => o.CreationDate).Skip(listVM.PageIndex)
                                                                .Take(listVM.PageSize);

            var result = query.Select(o => new OrderTrackingFlowDTO
            {
                Id = o.Id,
                OrderTrackingId = o.OrderTrackingId,
                OrderId = o.OrderTracking.OrderId,
                BankName = o.OrderTracking.Order.BankPort.Bank.Name,
                CustomerFullName = o.OrderTracking.Order.User.FirstName + " " + o.OrderTracking.Order.User.LastName,
                LastPositionId = o.ToPositionId.Value,
                LastToDocStateId = o.ToDocStateId,
                PaymentDate = o.OrderTracking.Order.CreationDate,
                Price = o.OrderTracking.Order.Price,
                LastDocStateTitle = o.ToDocState.Title,
            });

            return result;
        }

        #endregion

        #region Utilities

        private OrderTrackingFlow FirstStepFlow(Guid orderTrackingId)
        {
            var flow = new OrderTrackingFlow();
            flow.Id = Guid.NewGuid();
            flow.OrderTrackingId = orderTrackingId;
            flow.CreationDate = CommonHelper.GetCurrentDateTime();
            flow.ActionDate = CommonHelper.GetCurrentDateTime();
            flow.FromDocStateId = (int)OrderTrackingDocStateTypeEnum.InitialRegistration;
            flow.ToDocStateId = (int)OrderTrackingDocStateTypeEnum.InitialRegistration;
            flow.Description = "ثبت اولیه";
            return flow;
        }
        private OrderTrackingFlow SecondStepFlow(Guid orderTrackingId, Guid toPositionId)
        {
            var flow = new OrderTrackingFlow();
            flow.Id = Guid.NewGuid();
            flow.OrderTrackingId = orderTrackingId;
            flow.CreationDate = CommonHelper.GetCurrentDateTime();
            flow.FromDocStateId = (int)OrderTrackingDocStateTypeEnum.InitialRegistration;
            flow.ToDocStateId = (int)OrderTrackingDocStateTypeEnum.FinancialUnitReview;
            flow.ToPositionId = toPositionId;

            return flow;
        }
        #endregion
    }
}
