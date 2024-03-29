﻿using Lipar.Core.Infrastructure;
using Lipar.Web.Factories;
using Lipar.Web.Factories.Organization;
using Lipar.Web.Factories.Portal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using App = Lipar.Web.Factories.Application;

namespace Lipar.Web.Infrastructure
{
    public class FactoryModelStartup : ILiparStartup
    {
        public int Order => 1001;

        public void Configure(IApplicationBuilder application)
        {

        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICommonModelFactory, CommonModelFactory>();

            PortalConfigureServices(services);

            ApplicationConfigureService(services);

            OrganizationConfigureService(services);
        }

        public void PortalConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBlogModelFactory, BlogModelFactory>();
            services.AddTransient<IStaticPageModelFactory, StaticPageModelFactory>();
            services.AddTransient<IDynamicPageModelFactory, DynamicPageModelFactory>();
        }

        public void ApplicationConfigureService(IServiceCollection services)
        {
            services.AddTransient<App.IProductModelFactory, App.ProductModelFactory>();
            services.AddTransient<App.IShoppingCartItemModelFactory, App.ShoppingCartItemModelFactory>();
            services.AddTransient<App.IRedirectModelFactory, App.RedirectModelFactory>();
        }

        public void OrganizationConfigureService(IServiceCollection services)
        {
            services.AddTransient<IUserModelFactory, UserModelFactory>();
        }
    }
}
