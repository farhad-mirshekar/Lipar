using Lipar.Core;
using Lipar.Entities.Domain.Core;

namespace Lipar.Services.Core.Contracts
{
   public interface IEnabledTypeService
    {
        /// <summary>
        /// enabled type list method
        /// </summary>
        /// <returns></returns>
        IPagedList<EnabledType> List();
    }
}
