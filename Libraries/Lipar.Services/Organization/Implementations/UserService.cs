using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Core.Infrastructure;
using Lipar.Core.Security;
using Lipar.Data;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Organization.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.Organization.Implementations
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IRepository<User> _repository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserPasswordService _userPasswordService;
        #endregion

        #region Ctor
        public UserService(IRepository<User> repository
                           , IEncryptionService encryptionService
                           , IUserPasswordService userPasswordService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
            _userPasswordService = userPasswordService;
        }
        #endregion

        #region Methods
        public void Add(User model)
        {
            model.UserGuid = Guid.NewGuid();
            _repository.Insert(model);
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="model"></param>
        public void Delete(User model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var _workContext = EngineContext.Current.Resolve<IWorkContext>();
            model = GetById(model.Id);

            model.EnabledTypeId = (int)EnabledTypeEnum.InActive;
            model.RemoveDate = CommonHelper.GetCurrentDateTime();
            model.RemoverId = _workContext.CurrentUser.Id;

            Edit(model);
        }

        public void Edit(User model)
        {
            _repository.Update(model);
        }

        public User GetById(int Id)
        => _repository.GetById(Id);

        /// <summary>
        /// دریافت کاربر با ایمیل
        /// </summary>
        /// <param name="email">آدرس ایمیل</param>
        /// <returns></returns>
        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var user = _repository.Table.Where(u => u.Email == email.Trim()).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// دریافت کاربر با نام کاربری
        /// </summary>
        /// <param name="username">نام کاربری</param>
        /// <returns></returns>
        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            var user = _repository.Table.Where(u => u.Username == username.Trim()).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// دریافت لیست کاربران
        /// </summary>
        /// <param name="listVM">مدل جهت جست و جو</param>
        /// <returns></returns>
        public IPagedList<User> List(UserListVM listVM)
        {
            var query = _repository.Table;

            var models = new PagedList<User>(query, listVM.PageIndex, listVM.PageSize, false);

            return models;
        }

        /// <summary>
        /// جهت اعتبارسنجی برای لاگین
        /// </summary>
        /// <param name="usernameOrEmail">نام کاربری یا ایمیل</param>
        /// <param name="password">رمز عبور</param>
        /// <returns></returns>
        public LoginResultTypeEnum ValidateUser(string usernameOrEmail, string password)
        {
            var user = GetUserByUsername(usernameOrEmail);
            if (user == null)
                return LoginResultTypeEnum.UserNotExist;


            if (user.EnabledTypeId == (int)EnabledTypeEnum.InActive)
                return LoginResultTypeEnum.NotActive;

            if (user.CannotLoginUntilDate.HasValue && user.CannotLoginUntilDate.Value > DateTime.Now)
                return LoginResultTypeEnum.LockedOut;

            var getCurrentPassword = _userPasswordService.GetCurrentPassword(user.Id);

            if (!PasswordsMatch(getCurrentPassword, password))
            {
                //wrong password
                user.FailedLoginAttempts++;
                if (user.FailedLoginAttempts >= 3)
                {
                    //lock out
                    user.CannotLoginUntilDate = DateTime.UtcNow.AddMinutes(10);
                    //reset the counter
                    user.FailedLoginAttempts = 0;
                }

                Edit(user);
                return LoginResultTypeEnum.WrongPassword;
            }

            user.FailedLoginAttempts = 0;
            user.CannotLoginUntilDate = null;
            user.LastLoginDate = DateTime.Now;
            Edit(user);

            return LoginResultTypeEnum.Successful;
        }

        #endregion

        #region Utilities
        protected bool PasswordsMatch(UserPassword userPassword, string enteredPassword)
        {
            if (userPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;
            switch (userPassword.PasswordFormatTypeId)
            {
                case (int)PasswordFormatTypeEnum.Clear:
                    savedPassword = enteredPassword;
                    break;
                case (int)PasswordFormatTypeEnum.Encrypted:
                    savedPassword = _encryptionService.EncryptText(enteredPassword);
                    break;
                case (int)PasswordFormatTypeEnum.Hashed:
                    savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, userPassword.PasswordSalt, "SHA512");
                    break;
            }

            if (userPassword.Password == null)
                return false;

            return userPassword.Password.Equals(savedPassword);
        }
        #endregion
    }
}
