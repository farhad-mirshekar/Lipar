using Lipar.Entities.Domain.Organization;
using Lipar.Web.Areas.Dashboard.Models.Organization;

namespace Lipar.Web.Areas.Dashboard.Factories.Organization
{
    public interface IUserAddressModelFactory
    {
        /// <summary>
        /// prepare user address list model
        /// </summary>
        /// <returns></returns>
        public UserAddressListModel PrepareUserAddressListModel(int pageIndex = 1);

        /// <summary>
        /// prepare user address model
        /// </summary>
        /// <param name="userAddress">user address</param>
        /// <param name="model">model</param>
        /// <returns></returns>
        public UserAddressModel PrepareUserAddressModel(UserAddress userAddress, UserAddressModel model);
    }
}
