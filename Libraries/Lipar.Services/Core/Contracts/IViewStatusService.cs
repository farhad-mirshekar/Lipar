using Lipar.Core;
using Lipar.Entities.Domain.Core;

namespace Lipar.Services.Core.Contracts
{
   public interface IViewStatusService
    {
        /// <summary>
        /// view status list method
        /// </summary>
        /// <returns></returns>
        IPagedList<ViewStatus> List();
    }
}
