using Lipar.Core.Infrastructure;
using Lipar.Web.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Lipar.Web.Framework.Infrastructure
{
    public class AuthenticationStartup : ILiparStartup
    {
        public int Order => 30;

        public void Configure(IApplicationBuilder application)
        {
            application.UseLiparAuthentication();
            ConfigurationCulture(application);
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLiparAuthentication();
        }

        private void ConfigurationCulture(IApplicationBuilder application)
        {
            var options = application.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            var cookieProvider = options.Value.RequestCultureProviders
                .OfType<CookieRequestCultureProvider>()
                .First();
            var urlProvider = options.Value.RequestCultureProviders
                .OfType<QueryStringRequestCultureProvider>().First();

            cookieProvider.Options.DefaultRequestCulture = new RequestCulture("fa-IR");
            urlProvider.Options.DefaultRequestCulture = new RequestCulture("fa-IR");

            //cookieProvider.CookieName = CookieRequestCultureProvider.DefaultCookieName;

            options.Value.RequestCultureProviders.Clear();
            options.Value.RequestCultureProviders.Add(cookieProvider);
            options.Value.RequestCultureProviders.Add(urlProvider);
            application.UseRequestLocalization(options.Value);
        }
    }
}
