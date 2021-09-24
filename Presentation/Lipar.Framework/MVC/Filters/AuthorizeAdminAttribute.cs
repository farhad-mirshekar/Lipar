using Lipar.Services.Organization.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Lipar.Web.Framework.MVC.Filters
{
    public class AuthorizeAdminAttribute : TypeFilterAttribute
    {
        public AuthorizeAdminAttribute() : base(typeof(AuthorizeAdminFilter))
        {
        }

        private class AuthorizeAdminFilter : IAuthorizationFilter
        {
            private readonly ICommandService _commandService;
            public AuthorizeAdminFilter(ICommandService commandService)
            {
                _commandService = commandService;
            }

            public void OnAuthorization(AuthorizationFilterContext filterContext)
            {
                if (filterContext == null)
                    throw new ArgumentNullException(nameof(filterContext));

                //check whether this filter has been overridden for the action
                var actionFilter = filterContext.ActionDescriptor.FilterDescriptors
                    .Where(filterDescriptor => filterDescriptor.Scope == FilterScope.Action)
                    .Select(filterDescriptor => filterDescriptor.Filter).OfType<AuthorizeAdminAttribute>().FirstOrDefault();

                //there is AdminAuthorizeFilter, so check access
                if (filterContext.Filters.Any(filter => filter is AuthorizeAdminFilter))
                {
                    //authorize permission of access to the admin area
                    if (!_commandService.CheckPermission("AccessAdminPanel"))
                        filterContext.Result = new ChallengeResult();
                }
            }
        }
    }
}
