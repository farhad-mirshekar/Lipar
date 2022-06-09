using Lipar.Core.Infrastructure;
using Lipar.Web.Areas.Dashboard.Factories;
using Lipar.Web.Areas.Dashboard.Factories.Application;
using Lipar.Web.Areas.Dashboard.Factories.Organization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lipar.Web.Areas.Dashboard.Infrastructure
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

            OrganizationService(services);

            ApplicationService(services);
        }

        protected void OrganizationService(IServiceCollection services)
        {
            services.AddTransient<IUserAddressModelFactory, UserAddressModelFactory>();
        }

        protected void ApplicationService(IServiceCollection services)
        {
            services.AddTransient<IOrderModelFactory, OrderModelFactory>();
        }

    }
}
