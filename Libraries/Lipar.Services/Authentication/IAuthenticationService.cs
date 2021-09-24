using Lipar.Entities.Domain.Organization;

namespace Lipar.Services.Authentication
{
    public partial interface IAuthenticationService
    {
        void SignIn(User user, bool isPersistent);

        void SignOut();

        User GetAuthenticatedCustomer();
    }
}
