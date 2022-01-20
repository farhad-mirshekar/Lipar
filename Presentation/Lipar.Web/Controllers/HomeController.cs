using Lipar.Core.Common;
using Lipar.Data.Seeds;
using Lipar.Web.Areas.Admin.Models;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lipar.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Ctor
        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        public IActionResult Index()
        {
            //create shopping cart item cookie
            SetCookie(CookieDefaults.ShoppingCartItems);

            return View();
        }

        #region Utilities
        private void SetCookie(string cookieName)
        {
            cookieName = $"{CookieDefaults.Prefix}{cookieName}";
            if (_httpContextAccessor.HttpContext.Request.Cookies[cookieName] == null)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(24),
                    HttpOnly = true,
                    Secure = true
                };

                _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, "b75fd233-5194-4321-b4dd-b69e3648063f", cookieOptions);
            }
        }
        #endregion
    }
}