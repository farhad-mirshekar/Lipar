using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;

namespace Lipar.Services.Organization.Implementations
{
    public class CenterService : ICenterService
    {
        private readonly IRepository<Center> _repository;
        public CenterService(IRepository<Center> repository)
        {
            _repository = repository;
        }

        public void Add(Center model)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(Center model)
        {
            throw new System.NotImplementedException();
        }

        public Center GetById(int Id)
        {
            throw new System.NotImplementedException();
        }

        public IPagedList<Center> List(CenterListVM listVM)
        {
            var query = _repository.Table;

            var models = new PagedList<Center>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
    }
}
