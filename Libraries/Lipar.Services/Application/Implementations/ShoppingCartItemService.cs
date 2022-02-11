using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        #region Ctor
        public ShoppingCartItemService(IRepository<ShoppingCartItem> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields 
        private IRepository<ShoppingCartItem> _repository;
        #endregion

        #region Methods
        public void Add(ShoppingCartItem model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.ModifiedDate = CommonHelper.GetCurrentDateTime();

            _repository.Insert(model);
            
        }

        public void Delete(ShoppingCartItem model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(ShoppingCartItem model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.ModifiedDate = CommonHelper.GetCurrentDateTime();

            _repository.Update(model);
        }

        public ShoppingCartItem GetById(Guid Id, bool noTracking = false)
        {
            if(Id == Guid.Empty)
            {
                return null;
            }

            var query = _repository.Table;

            if (noTracking)
            {
                query = _repository.TableNoTracking;

                return query.FirstOrDefault(s => s.Id == Id);
            }

            return query.FirstOrDefault(s => s.Id == Id);
        }

        public ShoppingCartItem Get(Guid shoppingCartItemId, Guid? productId)
        {
            var query = _repository.TableNoTracking;

            query = query.Where(s => s.ShoppingCartItemId == shoppingCartItemId);

            if(productId.HasValue && productId.Value != Guid.Empty)
            {
                query = query.Where(s => s.ProductId == productId);
            }

            return query.FirstOrDefault();
        }

        public int GetCountShoppingCartItem(Guid shoppingCartItemId)
        {
            var query = _repository.TableNoTracking;

            return query.Where(x => x.ShoppingCartItemId == shoppingCartItemId).Count();
        }

        public IQueryable<ShoppingCartItem> GetShoppingCartItemQuery(Guid ShoppingCartItemId)
        {
            var query = _repository.TableNoTracking;

            return query.Where(x => x.ShoppingCartItemId == ShoppingCartItemId);
        }
        #endregion
    }
}
