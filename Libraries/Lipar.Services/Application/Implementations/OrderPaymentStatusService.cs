using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class OrderPaymentStatusService : IOrderPaymentStatusService
    {
        #region Ctor
        public OrderPaymentStatusService(IRepository<OrderPaymentStatus> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<OrderPaymentStatus> _repository;
        #endregion

        #region Methods
        public void Add(OrderPaymentStatus model)
        {
            if(model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.CreationDate = CommonHelper.GetCurrentDateTime();
            model.ModifiedDate = CommonHelper.GetCurrentDateTime();

            _repository.Insert(model);
        }

        public OrderPaymentStatus GetByShoppingCartItem(Guid shoppingCartItemId, bool noTracking = false)
        {
            if(shoppingCartItemId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(shoppingCartItemId));
            }

            var query = _repository.Table;
            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            var model = query.Where(o => o.ShoppingCartItemId == shoppingCartItemId)
                             .OrderByDescending(o => o.CreationDate)
                             .FirstOrDefault();

            return model;
        }

        public void Edit(OrderPaymentStatus model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.ModifiedDate = CommonHelper.GetCurrentDateTime();

            _repository.Update(model);
        }

        public OrderPaymentStatus GetById(Guid id, bool noTracking = false)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var query = _repository.Table;
            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            var model = query.Where(o => o.Id == id)
                             .FirstOrDefault();

            return model;
        }

        public OrderPaymentStatus GetByShoppingCartItem(Guid shoppingCartItemId, string token, bool noTracking = false)
        {
            if (shoppingCartItemId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(shoppingCartItemId));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("token is null");
            }

            var query = _repository.Table;
            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            var model = query.Where(o => o.ShoppingCartItemId == shoppingCartItemId
                                      && o.Token == token)
                             .OrderByDescending(o => o.CreationDate)
                             .FirstOrDefault();

            return model;
        }
        #endregion
    }
}
