using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.DTOs;
using Lipar.ViewModels.Application.Order;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Application.Contracts
{
    public interface IOrderTrackingFlowService
    {
        /// <summary>
        /// initial registration
        /// </summary>
        /// <param name="orderId"></param>
        void InitialRegistration(Guid orderId);

        /// <summary>
        /// get order tracking flow
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IQueryable<OrderTrackingFlowDTO> GetOrderTrackingFlows(OrderTrackingFlowListVM listVM);

        /// <summary>
        /// get order tracking flow
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        OrderTrackingFlow GetById(Guid Id , bool noTracking = false);
        IQueryable<OrderTrackingFlow> GetQuery();
        void Add(OrderTrackingFlow flow);
        void Edit(OrderTrackingFlow flow);
        OrderTrackingFlow GetById(Guid id, Guid? positionId, bool noTracking = false);

        /// <summary>
        /// get list order tracking flow for customer
        /// get list order tracking flow for customer
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        IList<OrderTrackingFlowViewModel> GetOrderTrackingFlowForCustomers(Guid orderId);

        Dictionary<int , string> GetOrderDocStates();
    }
}
