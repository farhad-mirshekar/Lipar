using Lipar.Core.Infrastructure;
using Lipar.Services.Organization.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lipar.Web.Framework.MVC.Filters
{
   public class CheckingPermissionsAttribute : TypeFilterAttribute
    {
        public CheckingPermissionsAttribute(string permissions) :base(typeof(CheckingPermissionsFilter))
        {
            Arguments = new object[] { permissions };
        }

        private class CheckingPermissionsFilter : IAuthorizationFilter
        {
            private string _permissions { get; set; }
            public CheckingPermissionsFilter(string permissions)
            {
                _permissions = permissions;
            }

            public void OnAuthorization(AuthorizationFilterContext filterContext)
            {
                if (string.IsNullOrEmpty(_permissions))
                {
                    filterContext.Result = new RedirectResult("~/Admin/Security/AccessDenid");
                    return;
                }

                var requiredPermissions = _permissions.Split(",");

                var _commandService = EngineContext.Current.Resolve<ICommandService>();

                foreach (var permission in requiredPermissions)
                {
                    var havePermission = _commandService.CheckPermission(permission);
                    if (!havePermission)
                    {
                        filterContext.Result = new RedirectResult("~/Admin/Security/AccessDenid");
                        return;
                    }

                    return;
                }
            }
        }
    }
}
