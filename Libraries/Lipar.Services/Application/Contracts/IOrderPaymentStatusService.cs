using Lipar.Entities.Domain.Application;
using System;

namespace Lipar.Services.Application.Contracts
{
    /// <summary>
    /// order payment status service
    /// </summary>
    public interface IOrderPaymentStatusService
    {
        /// <summary>
        /// add order payment status method
        /// </summary>
        /// <param name="model">OrderPaymentStatus</param>
        void Add(OrderPaymentStatus model);

        /// <summary>
        /// get by shopping cart item
        /// </summary>
        /// <param name="shoppingCartItemId"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        OrderPaymentStatus GetByShoppingCartItem(Guid shoppingCartItemId,bool noTracking = false);

        void Edit(OrderPaymentStatus model);

        OrderPaymentStatus GetById(Guid id , bool noTracking = false);

        OrderPaymentStatus GetByShoppingCartItem(Guid shoppingCartItemId,string token, bool noTracking = false);
    }
}
