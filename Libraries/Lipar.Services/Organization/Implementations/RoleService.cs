using Lipar.Core;
using Lipar.Core.Infrastructure;
using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lipar.Services.Organization.Implementations
{
    public class RoleService : IRoleService
    {
        #region Fields
        private readonly IRepository<Role> _repository;
        #endregion

        #region Ctor
        public RoleService(IRepository<Role> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(Role model)
        {
            var _workContext = EngineContext.Current.Resolve<IWorkContext>();
            model.CenterId = _workContext.CurrentCenter.Id;

            _repository.Insert(model);
        }

        public void Edit(Role model)
        {
            var _workContext = EngineContext.Current.Resolve<IWorkContext>();
            model.CenterId = _workContext.CurrentCenter.Id;

            _repository.Update(model);
        }

        public Role GetById(Guid Id)
        {
            var query = _repository.Table;

            query = query.Include(role => role.RolePermissions).ThenInclude(row => row.Command);

            var role = query.FirstOrDefault(r => r.Id == Id);

            if (role == null)
                return null;

            if ((role.RemoverId.HasValue && role.RemoverId.Value != Guid.Empty) || (role.RemoveDate.HasValue))
                return null;

            return role;
        }

        public IPagedList<Role> List(RoleListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (listVM.CenterId.HasValue && listVM.CenterId.Value != Guid.Empty)
                query = query.Where(r => r.CenterId == listVM.CenterId);

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(r => r.Name.Contains(listVM.Name.Trim()));

            var models = new PagedList<Role>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        #endregion
    }
}
