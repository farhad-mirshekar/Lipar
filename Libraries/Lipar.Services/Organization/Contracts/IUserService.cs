using Lipar.Core;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;

namespace Lipar.Services.Organization.Contracts
{
    public interface IUserService
    {
        User GetById(int Id);
        void Add(User model);
        void Edit(User model);
        IPagedList<User> List(UserListVM listVM);
        LoginResultTypeEnum ValidateUser(string usernameOrEmail, string password);
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        void Delete(User model);
        /// <summary>
        /// check duplicate username
        /// </summary>
        /// <param name="userName">username</param>
        /// <returns></returns>
        bool CheckDuplicateUserName(string userName);
    }
}
