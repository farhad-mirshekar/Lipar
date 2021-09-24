using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class DeliveryDateService : IDeliveryDateService
    {
        #region Ctor
        public DeliveryDateService(IRepository<DeliveryDate> repository
                                 , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IRepository<DeliveryDate> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Methods
        public void Add(DeliveryDate model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.UserId = _workContext.CurrentUser.Id;

            _repository.Insert(model);
        }

        public void Delete(DeliveryDate model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.RemoverId = _workContext.CurrentUser.Id;
            model.RemoveDate = CommonHelper.GetCurrentDateTime();

            Edit(model);
        }

        public void Edit(DeliveryDate model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.UserId = _workContext.CurrentUser.Id;

            _repository.Update(model);
        }

        public DeliveryDate GetById(int Id)
        {
            if(Id == 0)
            {
                return null;
            }

            return _repository.GetById(Id);
        }

        public IPagedList<DeliveryDate> List(DeliveryDateListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                query = query.Where(c => c.Name.Contains(listVM.Name.Trim()));
            }

            var models = new PagedList<DeliveryDate>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
