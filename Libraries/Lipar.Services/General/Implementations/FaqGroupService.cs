using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class FaqGroupService : IFaqGroupService
    {
        #region Fields
        private readonly IRepository<FaqGroup> _repository;
        #endregion

        #region Ctor
        public FaqGroupService(IRepository<FaqGroup> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(FaqGroup model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void Delete(FaqGroup model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
        }

        public void Edit(FaqGroup model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Update(model);
        }

        public FaqGroup GetById(Guid Id)
        => _repository.GetById(Id);

        public IPagedList<FaqGroup> List(FaqGroupListVM listVM)
        {
            var query = _repository.Table;

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(f => f.Name.Contains(listVM.Name.Trim()));

            var models = new PagedList<FaqGroup>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
