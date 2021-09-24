using Lipar.Core;
using Lipar.Entities.Domain.Application;

namespace Lipar.Services.Application.Contracts
{
   public interface IDiscountTypeService
    {
        /// <summary>
        /// discount type list method
        /// </summary>
        /// <returns></returns>
        IPagedList<DiscountType> List();
    }
}
