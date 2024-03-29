﻿using Lipar.Entities.Domain.Application;
using Lipar.ViewModels.Application;
using System;
using System.Linq;

namespace Lipar.Services.Application.Contracts
{
    /// <summary>
    /// order service
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// get all order setting
        /// </summary>
        /// <returns></returns>
        OrderSetting OrderSettings();

        /// <summary>
        /// add order method
        /// </summary>
        /// <param name="model">order</param>
        void Add(Order model);

        IQueryable<Order> GetQuery();

        OrderViewModel GetOrderViewModel(Guid orderId);
    }
}
