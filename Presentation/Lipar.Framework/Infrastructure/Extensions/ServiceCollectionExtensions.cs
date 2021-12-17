using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Core.Http;
using Lipar.Core.Infrastructure;
using Lipar.Services.Authentication;
using Lipar.Services.Caching;
using Lipar.Web.Framework.MVC.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lipar.Web.Framework.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static ILiparEngine ConfigureApplicationServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            CommonHelper.DefaultFileProvider = new LiparFileProvider(webHostEnvironment);

            var mvcCoreBuilder = services.AddMvcCore();

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //add all service singleton
            services.AddSingleton();

            //create engine and configure service provider
            var engine = EngineContext.Create();

            engine.ConfigureServices(services, configuration);

            return engine;
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddSingleton(this IServiceCollection services)
        {
            services.AddSingleton<ITypeFinder, AppDomainTypeFinder>();
            services.AddSingleton<ILiparEngine, LiparEngine>();
            services.AddSingleton<IRoutePublisher, RoutePublisher>();
            services.AddSingleton<ILiparFileProvider, LiparFileProvider>();
            services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
            services.AddSingleton<ICacheKeyService, CacheKeyService>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
        public static void AddLiparAuthentication(this IServiceCollection services)
        {
            //set default authentication schemes
            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = LiparAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = LiparAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = LiparAuthenticationDefaults.ExternalAuthenticationScheme;
            });

            //add main cookie authentication
            authenticationBuilder.AddCookie(LiparAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = $"{LiparCookieDefaults.Prefix}{LiparCookieDefaults.AuthenticationCookie}";
                options.Cookie.HttpOnly = true;
                options.LoginPath = LiparAuthenticationDefaults.LoginPath;
                options.AccessDeniedPath = LiparAuthenticationDefaults.AccessDeniedPath;

            });
        }
    }
}
