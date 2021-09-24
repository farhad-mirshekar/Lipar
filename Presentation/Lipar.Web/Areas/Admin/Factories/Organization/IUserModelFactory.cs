using Lipar.Entities.Domain.Organization;
using Lipar.Web.Areas.Admin.Models.Organization;

namespace Lipar.Web.Areas.Admin.Factories.Organization
{
    public interface IUserModelFactory
    {
        UserListModel PrepareUserListModel(UserSearchModel model);
        UserModel PrepareUserModel(UserModel model, User user);
    }
}
