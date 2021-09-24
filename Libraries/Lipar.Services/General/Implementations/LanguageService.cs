using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class LanguageService : ILanguageService
    {
        #region Fields
        private readonly IRepository<Language> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public LanguageService(IRepository<Language> repository
                             , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public void Add(Language model)
        {
            _repository.Insert(model);
        }

        public void Delete(Language model)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(Language model)
        {
            _repository.Update(model);
        }

        public Language GetById(int Id)
        => _repository.GetById(Id);

        public IPagedList<Language> List(LanguageListVM listVM)
        {
            var query = _repository.Table;

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(l => l.Name.Contains(listVM.Name.Trim()));

            var languages = new PagedList<Language>(query, listVM.PageIndex, listVM.PageSize, false);

            return languages;
        }

        #endregion
    }
}
