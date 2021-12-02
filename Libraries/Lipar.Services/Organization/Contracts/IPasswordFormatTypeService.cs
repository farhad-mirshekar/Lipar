using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Services.Organization.Contracts
{
    public interface IPasswordFormatTypeService
    {
        /// <summary>
        /// list password format type
        /// </summary>
        /// <returns></returns>
        IList<PasswordFormatType> List();
    }
}
