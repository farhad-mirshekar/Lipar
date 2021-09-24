using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;

namespace Lipar.Services.General.Implementations
{
    public class LanguageCultureService : ILanguageCultureService
    {
        #region Ctor
        public LanguageCultureService(IRepository<LanguageCulture> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<LanguageCulture> _repository;
        #endregion

        #region Methods
        public IPagedList<LanguageCulture> List()
        {
            var query = _repository.TableNoTracking;

            var models = new PagedList<LanguageCulture>(query);

            return models;
        }
        #endregion
    }
}
