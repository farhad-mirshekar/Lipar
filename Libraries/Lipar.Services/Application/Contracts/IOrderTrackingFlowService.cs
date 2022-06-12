using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Application.DTOs;
using System;
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
    }
}
