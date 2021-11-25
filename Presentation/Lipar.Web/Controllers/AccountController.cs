using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Authentication;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using Lipar.Web.Models.Organization;
using Lipar.Core.Common;
using Microsoft.AspNetCore.Http;

namespace Lipar.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Fileds

        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISettingService _settingService;
        #endregion

        #region Ctor
        public AccountController(IUserService userService
                               , IAuthenticationService authenticationService
                               , IActivityLogService activityLogService
                               , ILocaleStringResourceService localeStringResourceService
                               , IHttpContextAccessor httpContextAccessor
                               , ISettingService settingService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _httpContextAccessor = httpContextAccessor;
            _settingService = settingService;
        }
        #endregion

        #region Login / Register / SignOut
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model, string returnUrl, string captcha)
        {
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptcha").Value, out bool showCaptcha);
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptchaInLoginPage").Value, out bool showCaptchaInLoginPage);

            if (showCaptcha && showCaptchaInLoginPage)
            {
                var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.Captcha}";
                var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[cookieName];

                if (string.IsNullOrEmpty(cookieValue))
                {
                    ModelState.AddModelError("Captcha", _localeStringResourceService.GetResource("Account.Login.CaptchaInvalid"));
                }

                if (cookieValue != captcha)
                {
                    ModelState.AddModelError("Captcha", _localeStringResourceService.GetResource("Account.Login.CaptchaInvalid"));
                }
            }

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

                        _activityLogService.Add(user, "Web.Account.Login", _localeStringResourceService.GetResource("ActivityLog.Account.Login"), user);
                        //if return url is null, redirect default in home page
                        if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                            return RedirectToRoute("Homepage");

                        var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.Captcha}";
                        _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

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

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model.Password = model.Password.Trim();
                var isSafePassword = CommonHelper.IsSafePassword(model.Password);
            }

            return View();
        }
        public IActionResult CheckDuplicateUserName(string userName)
        {
            var check = _userService.CheckDuplicateUserName(userName);
            if (check)
            {
                return Json(_localeStringResourceService.GetResource("Web.Register.DuplicateUserName"));
            }
            return Json(true);
        }
        #endregion

    }
}