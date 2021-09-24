using Lipar.Core;
using Lipar.Entities.Domain.Organization;

namespace Lipar.Services.Organization.Contracts
{
   public interface ICommandTypeService
    {
        /// <summary>
        /// command type list method
        /// </summary>
        /// <returns></returns>
        IPagedList<CommandType> List();
    }
}
