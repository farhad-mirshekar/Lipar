using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using System.Linq;

namespace Lipar.Services.Organization.Implementations
{
    public class PositionRoleService : IPositionRoleService
    {
        #region Fields
        private readonly IRepository<PositionRole> _repository;
        #endregion

        #region Ctor
        public PositionRoleService(IRepository<PositionRole> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public IPagedList<PositionRole> List(PositionRoleListVM listVM)
        {
            var query = _repository.Table;

            if (listVM.RoleId.HasValue && listVM.RoleId.Value != 0)
                query = query.Where(pr => pr.RoleId == listVM.RoleId);

            if (listVM.PositionId.HasValue && listVM.PositionId.Value != 0)
                query = query.Where(pr => pr.PositionId == listVM.PositionId);

            var models = new PagedList<PositionRole>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }


        public void Add(PositionRole model)
        => _repository.Insert(model);

        public void Delete(PositionRole model)
        => _repository.Delete(model);

        #endregion
    }
}
