using Lipar.Entities.Domain.General;
using System.Collections.Generic;

namespace Lipar.Services.General.Contracts
{
    /// <summary>
    /// country service
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// get list country
        /// </summary>
        /// <returns>IEnumarable list</returns>
        IEnumerable<Country> List();
    }
}
