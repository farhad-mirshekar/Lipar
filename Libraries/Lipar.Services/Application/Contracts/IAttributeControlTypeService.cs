using Lipar.Core;
using Lipar.Entities.Domain.Application;

namespace Lipar.Services.Application.Contracts
{
   public interface IAttributeControlTypeService
    {
        /// <summary>
        /// attribute control type list method
        /// </summary>
        /// <returns></returns>
        IPagedList<AttributeControlType> List();
    }
}
