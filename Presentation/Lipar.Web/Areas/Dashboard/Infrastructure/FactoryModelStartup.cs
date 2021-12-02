using Lipar.Core.Infrastructure;
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
            OrganizationService(services);
        }

        protected void OrganizationService(IServiceCollection services)
        {
            services.AddTransient<IUserAddressModelFactory, UserAddressModelFactory>();
        }
    }
}
