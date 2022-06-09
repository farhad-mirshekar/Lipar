using Lipar.Core;
using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Core.Http;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.General;
using Lipar.Entities.Domain.General.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using Lipar.Services.Authentication;
using Lipar.Services.General.Contracts;
using Lipar.Services.Organization.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IPositionService _positionService;
        private readonly ICommandService _commandService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICenterService _centerService;
        private readonly ILanguageService _languageService;
        private readonly IStaticCacheManager _staticCacheManager;

        private User _cachedUser;
        private Position _cachedPosition;
        private IList<Command> _cachedCommand;
        private Language _cachedLanguage;
        private Guid _cachedShoppingCart;

        private CacheKey _cachKeyUser => new CacheKey(LiparCachingDefaults.LiparUser);
        private CacheKey _cachKeyPosition => new CacheKey(LiparCachingDefaults.LiparPosition);
        private CacheKey _cachKeyCommand => new CacheKey(LiparCachingDefaults.LiparCommand);
        private CacheKey _cachKeyShoppingCartItem => new CacheKey(LiparCachingDefaults.LiparShoppingCartItem);
        #endregion

        #region Ctor
        public WebWorkContext(IAuthenticationService authenticationService
                            , IPositionService positionService
                            , IHttpContextAccessor httpContextAccessor
                            , ICommandService commandService
                            , ICenterService centerService
                            , ILanguageService languageService
                            , IStaticCacheManager staticCacheManager)
        {
            _authenticationService = authenticationService;
            _positionService = positionService;
            _httpContextAccessor = httpContextAccessor;
            _commandService = commandService;
            _centerService = centerService;
            _languageService = languageService;
            _staticCacheManager = staticCacheManager;
        }
        #endregion

        #region Methods
        public User CurrentUser
        {
            get
            {
                return _staticCacheManager.Get(_cachKeyUser, () =>
                {
                    //whether there is a cached value
                    if (_cachedUser != null)
                        return _cachedUser;

                    User user = null;
                    if (user == null || user.EnabledTypeId == (int)EnabledTypeEnum.InActive)
                    {
                        //try to get registered user
                        user = _authenticationService.GetAuthenticatedCustomer();
                        _cachedUser = user;
                    }

                    if (user != null && user.EnabledTypeId == (int)EnabledTypeEnum.Active)
                        SetUserCookie(_cachedUser.UserGuid);

                    return _cachedUser;
                });

            }
            set
            {
                _staticCacheManager.Remove(_cachKeyUser);
                _cachedUser = value;
            }

        }
        public Position CurrentPosition
        {
            get
            {
                return _staticCacheManager.Get(_cachKeyPosition, () =>
                {
                    if (_cachedPosition != null)
                    {
                        return _cachedPosition;
                    }

                    if (CurrentUser != null && CurrentUser.UserTypeId == (int)UserTypeEnum.Admin)
                    {
                        var position = _positionService.GetActivePosition(CurrentUser.Id);

                        _cachedPosition = position;
                        return _cachedPosition;
                    }

                    return null;
                });
            }
            set
            {
                _staticCacheManager.Remove(_cachKeyPosition);

                _cachedPosition = value;
            }

        }
        public IEnumerable<Position> Positions
        {
            get
            {

                if (CurrentUser != null && CurrentUser.UserTypeId == (int)UserTypeEnum.Admin)
                {
                    var positions = _positionService.List(new PositionListVM { UserId = CurrentUser.Id });


                    return positions.Where(p => p.EnabledTypeId == (int)EnabledTypeEnum.Active).ToList();
                }
                return null;
            }
        }
        public IEnumerable<Command> Commands
        {
            get
            {
                return _staticCacheManager.Get(_cachKeyCommand, () =>
                {
                    if (_cachedCommand != null)
                    {
                        return _cachedCommand;
                    }

                    if (CurrentUser != null && CurrentUser.UserTypeId == (int)UserTypeEnum.Admin)
                    {
                        var roleId = CurrentPosition.PositionRoles.Select(r => r.RoleId).First();
                        var commands = _commandService.List(new CommandListVM { RoleId = roleId });

                        if (!commands.Any())
                            return null;

                        _cachedCommand = commands;
                        return _cachedCommand;
                    }
                    return null;
                });
            }
            set
            {
                _staticCacheManager.Remove(_cachKeyCommand);

                _cachedCommand = value.ToList();
            }
        }
        public Center CurrentCenter
        {
            get
            {
                return _centerService.List(new CenterListVM { }).Where(a => a.EnabledTypeId == (int)EnabledTypeEnum.Active).First();
            }
        }

        public Guid? ShoppingCartItems
        {
            get
            {
                return _staticCacheManager.Get<Guid?>(_cachKeyShoppingCartItem, () =>
                {
                    if (_cachedShoppingCart != Guid.Empty)
                    {
                        return _cachedShoppingCart;
                    }

                    var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.ShoppingCartItems}";
                    if (_httpContextAccessor.HttpContext.Request.Cookies[cookieName] != null)
                    {
                        var cacheKey = _httpContextAccessor.HttpContext.Request.Cookies[cookieName].ToString();
                        _cachedShoppingCart = Guid.Parse(cacheKey);

                        return _cachedShoppingCart;
                    }
                    var shoppingCartItemId = Guid.NewGuid();

                    SetShoppingCartCookie(shoppingCartItemId);

                    _cachedShoppingCart = shoppingCartItemId;

                    return _cachedShoppingCart;
                });
            }
            set
            {
                _staticCacheManager.Remove(_cachKeyShoppingCartItem);

                _cachedShoppingCart = value.HasValue ? value.Value : Guid.NewGuid();
            }
        }
        public Language WorkingLanguage
        {
            get
            {
                Language detectedLanguage = null;

                if (_cachedLanguage != null)
                {
                    return _cachedLanguage;
                }

                detectedLanguage = GetLanguageFromRequest();
                if (detectedLanguage != null)
                {
                    _cachedLanguage = detectedLanguage;
                    return _cachedLanguage;
                }
                else
                {
                    var languages = _languageService.List(new LanguageListVM { });

                    var language = languages.Where(x => x.LanguageCultureId == (int)LanguageCultureEnum.Persian).FirstOrDefault();

                    _cachedLanguage = language;
                    return _cachedLanguage;
                }
            }
            set
            {
                var languageId = value.Id;
                var language = _languageService.GetById(languageId, true);

                if (language != null && language.Id != 0)
                {
                    _cachedLanguage = language;
                }
                else
                {
                    _cachedLanguage = null;
                }
            }
        }
        #endregion

        #region Utilities
        protected virtual void SetUserCookie(Guid userGuid)
        {
            if (_httpContextAccessor.HttpContext?.Response == null)
                return;

            //delete current cookie value
            var cookieName = $"{LiparCookieDefaults.Prefix}{LiparCookieDefaults.UserCookie}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //get date of cookie expiration
            var cookieExpires = 24;
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (userGuid == Guid.Empty)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate,
                Secure = true
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, userGuid.ToString(), options);
        }
        protected Language GetLanguageFromRequest()
        {
            if (_httpContextAccessor.HttpContext?.Request == null)
                return null;

            var requestCulture = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture;

            //try to get language by culture name
            var requestLanguage = _languageService.List(new LanguageListVM { }).FirstOrDefault(language =>
                language.LanguageCulture.Seo.Equals("fa-IR", StringComparison.InvariantCultureIgnoreCase));

            //check language availability
            if (requestLanguage == null || requestLanguage.ViewStatusId != (int)ViewStatusEnum.Active)
                return null;

            return requestLanguage;
        }

        protected void SetShoppingCartCookie(Guid shoppingCartItemId)
        {
            if (_httpContextAccessor.HttpContext?.Response == null)
                return;

            //delete current cookie value
            var cookieName = $"{LiparCookieDefaults.Prefix}{CookieDefaults.ShoppingCartItems}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //get date of cookie expiration
            var cookieExpires = 24;
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (shoppingCartItemId == Guid.Empty)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate,
                Secure = true
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, shoppingCartItemId.ToString(), options);
        }
        #endregion
    }
}
