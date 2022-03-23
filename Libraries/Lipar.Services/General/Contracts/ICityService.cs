using Lipar.Entities.Domain.General;
using System.Collections.Generic;

namespace Lipar.Services.General.Contracts
{
    /// <summary>
    /// city service
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// get city list
        /// </summary>
        /// <param name="listVM">cityListVM</param>
        /// <returns>IEnumarable city</returns>
        IEnumerable<City> List(CityListVM listVM);
    }
}
