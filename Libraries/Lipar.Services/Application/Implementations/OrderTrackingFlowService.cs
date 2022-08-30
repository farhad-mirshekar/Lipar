using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.DTOs;
using Lipar.Entities.Domain.Application.Enums;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Application.Contracts;
using Lipar.ViewModels.Application.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class OrderTrackingFlowService : IOrderTrackingFlowService
    {
        #region Ctor
        public OrderTrackingFlowService(IRepository<OrderTrackingFlow> repository
                                      , IRepository<OrderTracking> orderTrackingRepository
                                      , IRepository<Position> positionRepository
                                      , IRepository<OrderTrackingDocState> orderTrackingDocStateRepository)
        {
            _repository = repository;
            _orderTrackingRepository = orderTrackingRepository;
            _positionRepository = positionRepository;
            _orderTrackingDocStateRepository = orderTrackingDocStateRepository;
        }
        #endregion

        #region Fields
        private readonly IRepository<OrderTrackingFlow> _repository;
        private readonly IRepository<OrderTracking> _orderTrackingRepository;
        private readonly IRepository<Position> _positionRepository;
        private readonly IRepository<OrderTrackingDocState> _orderTrackingDocStateRepository;
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

            if (listVM.OrderTrackingId.HasValue && listVM.OrderTrackingId.Value != Guid.Empty)
            {
                query = query.Where(o => o.OrderTrackingId == listVM.OrderTrackingId);
            }

            if (listVM.ToPositionId.HasValue && listVM.ToPositionId.Value != Guid.Empty)
            {
                query = query.Where(o => o.ToPositionId == listVM.ToPositionId);
            }

            if (listVM.ActionState.HasValue && listVM.ActionState.Value)
            {
                query = query.Where(o => !o.ActionDate.HasValue);
            }

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
                ActionDate = o.ActionDate,
                Description = o.Description,
                FromDocStateId = o.FromDocStateId,
                FromDocStateTitle = o.FromDocState.Title,
                FromPositionId = o.FromPositionId,
                FromPositionFullName = o.FromPositionId.HasValue
                                       ? o.FromPosition.User.FirstName + " " + o.FromPosition.User.LastName
                                       : "",
                ToDocStateId = o.ToDocStateId,
                ToDocStateTitle = o.ToDocState.Title,
                ToPositionId = o.ToPositionId,
                ToPositionFullName = o.ToPositionId.HasValue
                                       ? o.ToPosition.User.FirstName + " " + o.ToPosition.User.LastName
                                       : "",
            });

            return result;
        }

        public OrderTrackingFlow GetById(Guid id, bool noTracking = false)
        {
            var query = _repository.Table;
            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            var model = query.Where(otf => otf.OrderTrackingId == id)
                             .FirstOrDefault();

            return model;
        }

        public IQueryable<OrderTrackingFlow> GetQuery()
        {
            var query = _repository.TableNoTracking;

            return query;
        }

        public void Add(OrderTrackingFlow flow)
        {
            if (flow is null)
            {
                throw new ArgumentNullException(nameof(flow));
            }

            flow.Id = Guid.NewGuid();
            flow.CreationDate = CommonHelper.GetCurrentDateTime();
            _repository.Insert(flow);
        }

        public void Edit(OrderTrackingFlow flow)
        {
            if (flow is null)
            {
                throw new ArgumentNullException(nameof(flow));
            }

            _repository.Update(flow);
        }
        public OrderTrackingFlow GetById(Guid id, Guid? toPositionId, bool noTracking = false)
        {
            var query = _repository.Table;
            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            query = query.Where(otf => !otf.ActionDate.HasValue);

            query = query.Where(otf => otf.Id == id);

            if (toPositionId.HasValue && toPositionId.Value != Guid.Empty)
            {
                query = query.Where(otf => otf.ToPositionId == toPositionId);
            }

            var model = query.FirstOrDefault();

            return model;
        }

        public IList<OrderTrackingFlowViewModel> GetOrderTrackingFlowForCustomers(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new Exception("order id is invalid");
            }

            var query = _repository.TableNoTracking;
            query = query.Where(otf => otf.OrderTracking.OrderId == orderId);

            var result = query.Select(item => new OrderTrackingFlowViewModel
            {
                Id = item.Id,
                OrderTrackingId = item.OrderTrackingId,
                ActionDate = item.ActionDate,
                FromDocStateId = item.FromDocStateId,
                FromDocStateTitle = item.FromDocState.Title,
                ToDocStateTitle = item.ToDocState.Title,
                ToDocStateId = item.ToDocStateId
            }).ToList();

            return result;
        }

        public Dictionary<int, string> GetOrderDocStates()
        {
            var dic = new Dictionary<int, string>();
            var query = _orderTrackingDocStateRepository.TableNoTracking;

            var result = query.Select(item => new
            {
                item.Id,
                item.Title
            }).OrderBy(item => item.Id)
            .ToList();

            result.ForEach(item =>
            {
                dic.Add(item.Id, item.Title);
            });

            return dic;
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
