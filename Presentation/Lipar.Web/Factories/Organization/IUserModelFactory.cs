using Lipar.Entities.Domain.Organization;
using Lipar.Web.Models.Organization;

namespace Lipar.Web.Factories.Organization
{
    /// <summary>
    /// user model factory
    /// </summary>
    public interface IUserModelFactory
    {
        /// <summary>
        /// prepare passowrd recovery model
        /// <param name="PasswordRecoveryModel">password recovery model</param>
        /// </summary>
        /// <returns></returns>
        PasswordRecoveryModel PreparePasswordRecoveryModel(PasswordRecoveryModel model);

        /// <summary>
        /// prepare login model
        /// </summary>
        /// <param name="model">login model</param>
        /// <returns></returns>
        LoginModel PrepareLoginModel(LoginModel model);

        /// <summary>
        /// prepare user address model
        /// </summary>
        /// <param name="model">user address model</param>
        /// <param name="userAddress">user address</param>
        /// <returns>userAddressModel</returns>
        UserAddressModel PrepareUserAddressModel(UserAddressModel model, UserAddress userAddress);
    }
}
