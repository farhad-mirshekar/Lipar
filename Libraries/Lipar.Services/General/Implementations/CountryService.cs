using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class CountryService : ICountryService
    {
        #region Ctor
        public CountryService(IRepository<Country> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<Country> _repository;
        #endregion

        #region Methods
        public IEnumerable<Country> List()
        {
            var query = _repository.TableNoTracking;

            return query.ToList();
        }
        #endregion
    }
}
