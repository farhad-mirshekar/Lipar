using Lipar.Core;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Dashboard.Models.Organization;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Dashboard.Factories.Organization
{
    public class UserAddressModelFactory : IUserAddressModelFactory
    {
        #region Ctor
        public UserAddressModelFactory(IUserAddressService userAddressService
                                     , IWorkContext workContext)
        {
            _userAddressService = userAddressService;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IUserAddressService _userAddressService;
        private readonly IWorkContext _workContext;
        #endregion

        public UserAddressListModel PrepareUserAddressListModel(int pageIndex = 1)
        {
            var userAddress = _userAddressService.List(new UserAddressListVM
            {
                UserId = _workContext.CurrentUser.Id,
                PageIndex = pageIndex - 1,
                PageSize = 2
            });

            var userAddressListModel = new UserAddressListModel();
            var userAddressModels = userAddress.Select(u =>
            {
                var userAddressModel = new UserAddressModel();
                userAddressModel.Id = u.Id;
                userAddressModel.PostalCode = u.PostalCode;
                userAddressModel.UserId = u.UserId;
                userAddressModel.Address = u.Address;
                userAddressModel.CreationDate = u.CreationDate;

                return userAddressModel;
            });

            userAddressListModel.AvailableUserAddress = userAddressModels;
            userAddressListModel.PagingInfo = new PagingInfo { CurrentPage = pageIndex == 0 ? 1 : pageIndex, TotalItems = userAddress.TotalCount, ItemsPerPage = 2, Route = "areaRoute" };

            return userAddressListModel;
        }

        public UserAddressModel PrepareUserAddressModel(UserAddress userAddress, UserAddressModel model)
        {
            if(userAddress != null)
            {
                model.Address = userAddress.Address;
                model.CreationDate = userAddress.CreationDate;
                model.Id = userAddress.Id;
                model.PostalCode = userAddress.PostalCode;
                model.UserId = userAddress.UserId;
            }

            return model;
        }
    }
}
