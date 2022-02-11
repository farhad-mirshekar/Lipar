using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Authentication;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using Lipar.Web.Models.Organization;
using Lipar.Core.Common;
using Microsoft.AspNetCore.Http;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Web.Factories.Organization;
using System;
using Lipar.Services.Organization;
using Lipar.Services.Messages;
using Lipar.Core;

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
        private readonly IUserPasswordService _passwordService;
        private readonly IUserModelFactory _userModelFactory;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public AccountController(IUserService userService
                               , IAuthenticationService authenticationService
                               , IActivityLogService activityLogService
                               , ILocaleStringResourceService localeStringResourceService
                               , IHttpContextAccessor httpContextAccessor
                               , ISettingService settingService
                               , IUserPasswordService passwordService
                               , IUserModelFactory userModelFactory
                               , IGenericAttributeService genericAttributeService
                               , IWorkflowMessageService workflowMessageService
                               , IWorkContext workContext)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _httpContextAccessor = httpContextAccessor;
            _settingService = settingService;
            _passwordService = passwordService;
            _userModelFactory = userModelFactory;
            _genericAttributeService = genericAttributeService;
            _workflowMessageService = workflowMessageService;
            _workContext = workContext;
        }
        #endregion

        #region Login / Register / SignOut
        public IActionResult Login()
        {
            var loginModel = new LoginModel();

            _userModelFactory.PrepareLoginModel(loginModel);

            return View(loginModel);
        }

        [HttpPost]
        public IActionResult Login(LoginModel model, string returnUrl, string captcha)
        {
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptcha")?.Value, out bool showCaptcha);
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptchaInLoginPage")?.Value, out bool showCaptchaInLoginPage);

            if (showCaptcha && showCaptchaInLoginPage)
            {
                var cookieName = $"{CookieDefaults.Prefix}.Login{CookieDefaults.Captcha}";
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
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptcha").Value, out bool showCaptcha);
            bool.TryParse(_settingService.GetSetting("CommonSetting.ShowCaptchaInRegisterPage").Value, out bool showCaptchaInRegisterPage);

            if (showCaptcha && showCaptchaInRegisterPage)
            {
                var cookieName = $"{CookieDefaults.Prefix}.Register{CookieDefaults.Captcha}";
                var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[cookieName];

                if (string.IsNullOrEmpty(cookieValue))
                {
                    ModelState.AddModelError("Captcha", _localeStringResourceService.GetResource("Account.Login.CaptchaInvalid"));
                }

                if (cookieValue != model.Captcha)
                {
                    ModelState.AddModelError("Captcha", _localeStringResourceService.GetResource("Account.Login.CaptchaInvalid"));
                }
            }

            if (ModelState.IsValid)
            {
                //check password safe
                model.Password = model.Password.Trim();
                var isSafePassword = CommonHelper.IsSafePassword(model.Password);
                if (!isSafePassword)
                {
                    ModelState.AddModelError("Password", _localeStringResourceService.GetResource("Account.Login.Password.IsNotSafe"));
                    return View(model);
                }

                var user = new User
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    CellPhone = model.CellPhone,
                    CellPhoneVerified = true,
                    NationalCode = model.NationalCode,
                    Username = model.UserName,
                    UserTypeId = (int)UserTypeEnum.Users_Outside_TheOrganization,
                    EnabledTypeId = (int)EnabledTypeEnum.Active,
                };

                _userService.Add(user);

                //add activity log for create user
                _activityLogService.Add("Account.User.Create", string.Format(_localeStringResourceService.GetResource("ActivityLog.Account.User.Create"), user.Username, user.NationalCode), user);

                var password = new UserPassword
                {
                    Password = model.Password,
                    PasswordFormatTypeId = (int)PasswordFormatTypeEnum.Encrypted,
                    UserId = user.Id
                };

                _passwordService.Add(password);

                //add activity log for create password
                _activityLogService.Add("Account.UserPassword.Create", string.Format(_localeStringResourceService.GetResource("ActivityLog.Account.UserPassword.Create"), user.Id), password);

                //create cookie
                _authenticationService.SignIn(user, false);

                //if return url is null, redirect default in home page
                if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                {
                    return RedirectToRoute("Homepage");
                }
            }

            return View(model);
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

        #region Password Recovery
        public IActionResult PasswordRecovery()
        {
            var model = new PasswordRecoveryModel();
            model = _userModelFactory.PreparePasswordRecoveryModel(model);

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult PasswordRecovery(PasswordRecoveryModel model)
        {
            var condination = new PasswordRecoveryModel();
            condination = _userModelFactory.PreparePasswordRecoveryModel(condination);

            if (condination.ShowCaptcha && condination.ShowCaptchaInPasswordRecoveryPage)
            {
                var cookieName = $"{CookieDefaults.Prefix}.PasswordRecovery{CookieDefaults.Captcha}";
                var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[cookieName];

                if (string.IsNullOrEmpty(cookieValue))
                {
                    ModelState.AddModelError("Captcha", _localeStringResourceService.GetResource("Account.Login.CaptchaInvalid"));
                }

                if (cookieValue != model.Captcha)
                {
                    ModelState.AddModelError("Captcha", _localeStringResourceService.GetResource("Account.Login.CaptchaInvalid"));
                }
            }

            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByEmail(model.Email);
                if (user != null && user.EnabledTypeId == (int)EnabledTypeEnum.Active)
                {
                    var passwordRecoveryToken = Guid.NewGuid();
                    _genericAttributeService.SaveAttribute(user, LiparOrganizationDefaults.PasswordRecoveryTokenAttribute, passwordRecoveryToken.ToString());

                    var passwordRecoveryTokenExpire = CommonHelper.GetCurrentDateTime().AddHours(5);
                    _genericAttributeService.SaveAttribute(user, LiparOrganizationDefaults.PasswordRecoveryTokenDateGeneratedAttribute, passwordRecoveryTokenExpire.ToString());

                    _workflowMessageService.SendPasswordRecoveryMessage(user, _workContext.WorkingLanguage.Id);
                }
                else
                {
                    model.Result = _localeStringResourceService.GetResource("Account.PasswordRecovery.EmailNotFound");
                }
            }

            model = _userModelFactory.PreparePasswordRecoveryModel(model);

            return View(model);
        }
        #endregion

    }
}