using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
   public interface IContactUsTypeService
    {
        /// <summary>
        /// contact us type list method
        /// </summary>
        /// <returns></returns>
        IPagedList<ContactUsType> List();
    }
}
