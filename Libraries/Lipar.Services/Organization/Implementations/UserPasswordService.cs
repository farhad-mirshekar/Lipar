using Lipar.Core.Security;
using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Organization.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Organization.Implementations
{
    public class UserPasswordService : IUserPasswordService
    {
        #region Fields
        private readonly IRepository<UserPassword> _repository;
        private readonly IEncryptionService _encryptionService;
        #endregion

        #region Ctor
        public UserPasswordService(IRepository<UserPassword> repository
                                 , IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        #endregion

        #region Methods

        public void Add(UserPassword model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            switch (model.PasswordFormatTypeId)
            {
                case (int)PasswordFormatTypeEnum.Encrypted:
                    model.Password = _encryptionService.EncryptText(model.Password, "4041158806227258");
                    break;
                case (int)PasswordFormatTypeEnum.Hashed:
                    var saltKey = _encryptionService.CreateSaltKey(5);
                    model.PasswordSalt = saltKey;
                    model.Password = _encryptionService.CreatePasswordHash(model.Password, model.PasswordSalt, "SHA512");
                    break;
            }
            _repository.Insert(model);
        }

        public void Edit(UserPassword model)
        => _repository.Update(model);

        public UserPassword GetCurrentPassword(Guid UserId)
        {
            if (UserId == Guid.Empty)
                return null;

            return GetCustomerPasswords(UserId, passwordsToReturn: 1).FirstOrDefault();
        }

        public IEnumerable<UserPassword> GetCustomerPasswords(Guid? userId = null, PasswordFormatTypeEnum? passwordFormatType = null, int? passwordsToReturn = null)
        {
            var query = _repository.Table;

            if (userId.HasValue)
                query = query.Where(password => password.UserId == userId.Value);

            if (passwordFormatType.HasValue)
                query = query.Where(password => password.PasswordFormatTypeId == (int)passwordFormatType.Value);

            if (passwordsToReturn.HasValue)
                query = query.OrderByDescending(password => password.CreationDate).Take(passwordsToReturn.Value);

            return query;
        }
    }
    #endregion
}
