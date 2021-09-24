using Lipar.Core;
using Lipar.Core.Http;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Authentication;
using Lipar.Services.Organization.Contracts;
using Microsoft.AspNetCore.Http;
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

        private User _cachedUser;
        private Position _cachedPosition;
        private IList<Command> _cachedCommand;
        #endregion

        #region Ctor
        public WebWorkContext(IAuthenticationService authenticationService
                            , IPositionService positionService
                            , IHttpContextAccessor httpContextAccessor
                            , ICommandService commandService
                            , ICenterService centerService)
        {
            _authenticationService = authenticationService;
            _positionService = positionService;
            _httpContextAccessor = httpContextAccessor;
            _commandService = commandService;
            _centerService = centerService;
        }
        #endregion

        #region Methods
        public User CurrentUser
        {
            get
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
            }
            set
            {
                SetUserCookie(value.UserGuid);
                _cachedUser = value;
            }
        }
        public Position CurrentPosition
        {
            get
            {
                if (_cachedPosition != null)
                {
                    return _cachedPosition;
                }

                if (CurrentUser != null)
                {
                    var positions = _positionService.List(new PositionListVM { UserId = CurrentUser.Id });
                    var position = positions.Where(p => p.EnabledTypeId == (int)EnabledTypeEnum.Active && p.Default == true).FirstOrDefault();

                    _cachedPosition = position;
                    return _cachedPosition;
                }

                return null;
            }
            set
            {
                _cachedPosition = value;
            }

        }
        public IEnumerable<Position> Positions
        {
            get
            {

                if (CurrentUser != null)
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
                if(_cachedCommand != null)
                {
                    return _cachedCommand;
                }

                if (CurrentUser != null)
                {
                    var roleId = CurrentPosition.PositionRoles.Select(r => r.RoleId).First();
                    var commands = _commandService.List(new CommandListVM { RoleId = roleId });

                    if (commands.Count() == 0)
                        return null;

                    _cachedCommand = commands;
                    return _cachedCommand;
                }
                return null;
            }
            set
            {
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
        #endregion
    }
}
