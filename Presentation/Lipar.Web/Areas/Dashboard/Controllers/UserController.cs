using Lipar.Core;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Dashboard.Models.Organization;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Areas.Dashboard.Controllers
{
    public class UserController : BaseDashboardController
    {
        #region Ctor
        public UserController(IUserService userService
                            , IWorkContext workContext
                            , INotificationService notificationService)
        {
            _userService = userService;
            _workContext = workContext;
            _notificationService = notificationService;
        }
        #endregion

        #region Fields
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        private readonly INotificationService _notificationService;
        #endregion
        public IActionResult Profile()
        {
            if(_workContext.CurrentUser == null)
            {
                return AccessDeniedView();
            }
            var user = _userService.GetById(_workContext.CurrentUser.Id);

            if(user == null)
            {
                return NotFound();
            }

            var model = new ProfileModel
            {
                Id = user.Id,
                CellPhone = user.CellPhone,
                CreationDate = user.CreationDate,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NationalCode = user.NationalCode,
                Username = user.Username,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Profile(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetById(model.Id);
                if(user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName.Trim();
                user.LastName = model.LastName.Trim();
                user.NationalCode = model.NationalCode.Trim();
                user.Email = model.Email.Trim();
                user.CellPhone = model.CellPhone.Trim();

                _userService.Edit(user);


            }

            return View(model);
        }
    }
}
