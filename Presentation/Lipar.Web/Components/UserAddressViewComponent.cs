using Lipar.Core;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Models.Organization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lipar.Web.Components
{
    public class UserAddressViewComponent : ViewComponent
    {
        #region Ctor
        public UserAddressViewComponent(IUserAddressService userAddressService
                                      , IWorkContext workContext)
        {
            _userAddressService = userAddressService;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IWorkContext _workContext;
        private readonly IUserAddressService _userAddressService;
        #endregion

        #region Methods
        public IViewComponentResult Invoke()
        {
            var userAddressList = _userAddressService.List(new UserAddressListVM
            {
                UserId = _workContext.CurrentUser.Id
            });

            var availableUserAddress = new List<UserAddressModel>();
            foreach (var address in userAddressList)
            {
                availableUserAddress.Add(new UserAddressModel
                {
                    Address = address.Address,
                    Id = address.Id,
                    CreationDate = address.CreationDate,
                    PostalCode = address.PostalCode,
                    UserId = address.UserId,
                });
            }
            return View(availableUserAddress);
        }
        #endregion
    }
}
