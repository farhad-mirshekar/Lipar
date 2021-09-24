using Lipar.Entities;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using System.Collections.Generic;

namespace Lipar.Services.Organization.Contracts
{
    public interface IUserPasswordService
    {
        UserPassword GetCurrentPassword(int UserId);
        IEnumerable<UserPassword> GetCustomerPasswords(int? customerId = null, PasswordFormatTypeEnum? passwordFormatType = null, int? passwordsToReturn = null);
        void Add(UserPassword model);
        void Edit(UserPassword model);
    }
}
