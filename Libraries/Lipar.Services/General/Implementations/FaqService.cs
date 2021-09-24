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
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public FaqService(IRepository<Faq> repository
                        , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public void Add(Faq model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.UserId = _workContext.CurrentUser.Id;

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

            model.UserId = _workContext.CurrentUser.Id;

            _repository.Update(model);
        }

        public Faq GetById(int Id)
        => _repository.GetById(Id);

        public IPagedList<Faq> List(FaqListVM listVM)
        {
            var query = _repository.Table;

            if (listVM.FaqGroupId.HasValue && listVM.FaqGroupId.Value != 0)
                query = query.Where(f => f.FaqGroupId == listVM.FaqGroupId);

            var models = new PagedList<Faq>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }
        #endregion
    }
}
