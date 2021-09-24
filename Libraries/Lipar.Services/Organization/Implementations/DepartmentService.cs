using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using System.Linq;

namespace Lipar.Services.Organization.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IRepository<Department> _repository;
        #endregion

        #region Ctor
        public DepartmentService(IRepository<Department> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public IPagedList<Department> List(DepartmentListVM listVM)
        {
            var query = _repository.Table;

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(d => d.Name.Contains(listVM.Name.Trim()));

            var models = new PagedList<Department>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
