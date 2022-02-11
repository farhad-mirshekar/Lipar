using Lipar.Entities;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using System;
using System.Collections.Generic;

namespace Lipar.Services.Organization.Contracts
{
    public interface IUserPasswordService
    {
        UserPassword GetCurrentPassword(Guid UserId);
        IEnumerable<UserPassword> GetCustomerPasswords(Guid? customerId = null, PasswordFormatTypeEnum? passwordFormatType = null, int? passwordsToReturn = null);
        void Add(UserPassword model);
        void Edit(UserPassword model);
    }
}
