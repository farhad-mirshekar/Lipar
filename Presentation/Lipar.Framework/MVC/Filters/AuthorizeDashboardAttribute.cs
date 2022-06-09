using Lipar.Core;
using Lipar.Entities.Domain.Organization.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Lipar.Web.Framework.MVC.Filters
{
   public class AuthorizeDashboardAttribute : TypeFilterAttribute
    {
        public AuthorizeDashboardAttribute():base(typeof(AuthorizeDashboardFilter))
        {

        }

        private class AuthorizeDashboardFilter : IAuthorizationFilter
        {
            private readonly IWorkContext _workContext;
            public AuthorizeDashboardFilter(IWorkContext workContext)
            {
                _workContext = workContext;
            }

            public void OnAuthorization(AuthorizationFilterContext filterContext)
            {
                if (filterContext == null)
                    throw new ArgumentNullException(nameof(filterContext));

                //check whether this filter has been overridden for the action
                var actionFilter = filterContext.ActionDescriptor.FilterDescriptors
                    .Where(filterDescriptor => filterDescriptor.Scope == FilterScope.Action)
                    .Select(filterDescriptor => filterDescriptor.Filter).OfType<AuthorizeAdminAttribute>().FirstOrDefault();

                //there is DashboardAuthorizeFilter, so check access
                if (filterContext.Filters.Any(filter => filter is AuthorizeDashboardFilter))
                {
                    //authorize permission of access to the admin area
                    if (_workContext.CurrentUser == null || !(_workContext.CurrentUser != null && _workContext.CurrentUser.UserTypeId != (int)UserTypeEnum.Admin))
                        filterContext.Result = new ChallengeResult();
                }
            }
        }
    }
}
