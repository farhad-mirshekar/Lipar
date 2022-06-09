using Lipar.Core.Infrastructure;
using Lipar.Services.General.Contracts;
using Lipar.Web.Framework.MVC.Routing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lipar.Web.Framework.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void LiparStaticFiles(this IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseBrowserLink();
        }

        public static void UseLiparAuthentication(this IApplicationBuilder application)
        {
            application.UseMiddleware<AuthenticationMiddleware>();
        }

        public static void UseLiparEndpoints(this IApplicationBuilder application)
        {
            //Add the EndpointRoutingMiddleware
            application.UseRouting();

            //Execute the endpoint selected by the routing middleware
            application.UseEndpoints(endpoints =>
            {
                //register all routes
                EngineContext.Current.Resolve<IRoutePublisher>().RegisterRoutes(endpoints);
            });
        }

        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }
    }
}
