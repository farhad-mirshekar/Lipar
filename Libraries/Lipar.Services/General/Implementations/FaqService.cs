using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class FaqService : IFaqService
    {
        #region Fields
        private readonly IRepository<Faq> _repository;
        #endregion

        #region Ctor
        public FaqService(IRepository<Faq> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(Faq model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void Delete(Faq model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
        }

        public void Edit(Faq model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Update(model);
        }

        public Faq GetById(Guid Id)
        => _repository.GetById(Id);

        public IPagedList<Faq> List(FaqListVM listVM)
        {
            var query = _repository.Table;

            if (listVM.FaqGroupId.HasValue && listVM.FaqGroupId.Value != Guid.Empty)
                query = query.Where(f => f.FaqGroupId == listVM.FaqGroupId);

            var models = new PagedList<Faq>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
