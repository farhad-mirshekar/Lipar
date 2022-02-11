using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Organization.Implementations
{
    public class UserAddressService : IUserAddressService
    {
        #region Ctor
        public UserAddressService(IRepository<UserAddress> repository
                                , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IRepository<UserAddress> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Methods
        public void Add(UserAddress model)
        {
            if(model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.CreationDate = CommonHelper.GetCurrentDateTime();
            model.UserId = _workContext.CurrentUser.Id;

            _repository.Insert(model);
        }

        public void Delete(UserAddress model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(UserAddress model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public UserAddress GetById(Guid Id,bool noTracking = false)
        {
            if(Id == Guid.Empty)
            {
                return null;
            }

            var query = _repository.Table;
            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            return query.FirstOrDefault(u => u.Id == Id);
        }

        public IPagedList<UserAddress> List(UserAddressListVM listVM)
        {
            var query = _repository.TableNoTracking;

            query = query.Where(u => u.UserId == listVM.UserId);

            var models = new PagedList<UserAddress>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
