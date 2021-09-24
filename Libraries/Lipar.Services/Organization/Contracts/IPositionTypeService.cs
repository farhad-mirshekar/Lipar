using Lipar.Core;
using Lipar.Entities.Domain.Organization;

namespace Lipar.Services.Organization.Contracts
{
   public interface IPositionTypeService
    {
        /// <summary>
        /// position type list method
        /// </summary>
        /// <returns></returns>
        IPagedList<PositionType> List();
    }
}
