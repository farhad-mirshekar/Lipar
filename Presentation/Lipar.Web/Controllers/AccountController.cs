using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Authentication;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Framework.Controllers;
using Lipar.Web.Models.Organization.User;
using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Fileds

        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        #endregion

        #region Ctor
        public AccountController(IUserService userService
                               , IAuthenticationService authenticationService
                               , IActivityLogService activityLogService
                               , ILocaleStringResourceService localeStringResourceService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        #endregion

        #region Login / Register / SignOut
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model.Username = model.Username.Trim();

                var validateUser = _userService.ValidateUser(model.Username, model.Password);

                switch (validateUser)
                {
                    case LoginResultTypeEnum.Successful:
                        //find user by username
                        var user = _userService.GetUserByUsername(model.Username);

                        //create cookie
                        _authenticationService.SignIn(user, model.RememberMe);

                        _activityLogService.Add(user,"Web.Account.Login" , _localeStringResourceService.GetResource("ActivityLog.Account.Login"), user);
                        //if return url is null, redirect default in home page
                        if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                            return RedirectToRoute("Homepage");

                        return Redirect(returnUrl);

                    case LoginResultTypeEnum.UserNotExist:
                        ModelState.AddModelError("", "Account.Login.WrongCredentials.CustomerNotExist");
                        break;
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            _authenticationService.SignOut();

            return RedirectToRoute("Homepage");
        }
        #endregion

    }
}