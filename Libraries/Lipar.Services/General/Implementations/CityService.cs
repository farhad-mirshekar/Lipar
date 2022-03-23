using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class CityService : ICityService
    {
        #region Ctor
        public CityService(IRepository<City> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<City> _repository;
        #endregion

        #region Methods
        public IEnumerable<City> List(CityListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if(listVM.ProvinceId.HasValue && listVM.ProvinceId.Value != 0)
            {
                query = query.Where(c => c.ProvinceId == listVM.ProvinceId);
            }

            return query.ToList();
        }
        #endregion
    }
}
