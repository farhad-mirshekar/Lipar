using Lipar.Core.Infrastructure;
using Lipar.Web.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lipar.Web.Framework.Infrastructure
{
    public class AuthenticationStartup : ILiparStartup
    {
        public int Order => 30;

        public void Configure(IApplicationBuilder application)
        {
            application.UseLiparAuthentication();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLiparAuthentication();
        }
    }
}
