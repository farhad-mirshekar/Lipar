using Lipar.Entities.Domain.General;
using System.Collections.Generic;

namespace Lipar.Services.General.Contracts
{
    /// <summary>
    /// province service
    /// </summary>
    public interface IProvinceService
    {
        /// <summary>
        /// get province list
        /// </summary>
        /// <param name="listVM">provinceListVM</param>
        /// <returns>IEnumarable province</returns>
        IEnumerable<Province> List(ProvinceListVM listVM);
    }
}
