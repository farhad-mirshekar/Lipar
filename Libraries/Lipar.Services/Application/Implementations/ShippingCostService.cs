using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
   public class ShippingCostService : IShippingCostService
    {
        #region Ctor
        public ShippingCostService(IRepository<ShippingCost> repository
                                 , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IRepository<ShippingCost> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Methods
        public void Add(ShippingCost model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.UserId = _workContext.CurrentUser.Id;

            _repository.Insert(model);
        }

        public void Delete(ShippingCost model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.RemoverId = _workContext.CurrentUser.Id;
            model.RemoveDate = CommonHelper.GetCurrentDateTime();

            Edit(model);
        }

        public void Edit(ShippingCost model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.UserId = _workContext.CurrentUser.Id;

            _repository.Update(model);
        }

        public ShippingCost GetById(int Id)
        {
            if (Id == 0)
            {
                return null;
            }

            return _repository.GetById(Id);
        }

        public IPagedList<ShippingCost> List(ShippingCostListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                query = query.Where(c => c.Name.Contains(listVM.Name.Trim()));
            }

            var models = new PagedList<ShippingCost>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
