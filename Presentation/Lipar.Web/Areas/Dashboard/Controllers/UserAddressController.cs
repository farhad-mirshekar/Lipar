using Lipar.Entities.Domain.Organization;
using Lipar.Services.General.Contracts;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Dashboard.Factories.Organization;
using Lipar.Web.Areas.Dashboard.Models.Organization;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lipar.Web.Areas.Dashboard.Controllers
{
    public class UserAddressController : BaseDashboardController
    {
        #region Ctor
        public UserAddressController(IUserAddressService userAddressService
                                   , IUserAddressModelFactory userAddressModelFactory
                                   , INotificationService notificationService
                                   , ILocaleStringResourceService localeStringResourceService)
        {
            _userAddressService = userAddressService;
            _userAddressModelFactory = userAddressModelFactory;
            _notificationService = notificationService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Fields
        private readonly IUserAddressService _userAddressService;
        private readonly IUserAddressModelFactory _userAddressModelFactory;
        private readonly INotificationService _notificationService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Methods
        public IActionResult Index()
            => RedirectToAction("List");

        public IActionResult List(int? page)
        {
            page = page ?? 1;

            var model = _userAddressModelFactory.PrepareUserAddressListModel(page.Value);
            
            return View(model);
        }
        public IActionResult Create()
        {
            var model = _userAddressModelFactory.PrepareUserAddressModel(null, new UserAddressModel());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserAddressModel model)
        {
            if (ModelState.IsValid)
            {
                var userAddress = new UserAddress
                {
                    Address = model.Address,
                    PostalCode = model.PostalCode
                };

                _userAddressService.Add(userAddress);

                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Notification.Success.EntityCreate"));

                return RedirectToAction("Index");
            }

            model = _userAddressModelFactory.PrepareUserAddressModel(null, model);
            return View(model);
        }

        public IActionResult Edit(Guid Id)
        {
            if(Id == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var userAddress = _userAddressService.GetById(Id, true);
            if(userAddress == null)
            {
                return RedirectToAction("List");
            }

            var model = _userAddressModelFactory.PrepareUserAddressModel(userAddress, new UserAddressModel());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserAddressModel model)
        {
            if (ModelState.IsValid)
            {
                var userAddress = _userAddressService.GetById(model.Id);

                userAddress.Address = model.Address.Trim();
                userAddress.PostalCode = model.PostalCode.Trim();

                _userAddressService.Edit(userAddress);

                _notificationService.SusscessNotification(_localeStringResourceService.GetResource("Notification.Success.EntityEdit"));

                return RedirectToAction("Index");
            }

            model = _userAddressModelFactory.PrepareUserAddressModel(null, model);
            return View(model);
        }
        #endregion
    }
}
