using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class ProvinceService : IProvinceService
    {
        #region Ctor
        public ProvinceService(IRepository<Province> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<Province> _repository;
        #endregion

        #region Methods
        public IEnumerable<Province> List(ProvinceListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if(listVM.CountryId.HasValue && listVM.CountryId.Value != 0)
            {
                query = query.Where(p => p.CountryId == listVM.CountryId);
            }

            return query.ToList();
        }
        #endregion
    }
}
