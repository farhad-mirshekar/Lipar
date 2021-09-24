using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lipar.Core.Infrastructure
{
    public interface ILiparEngine
    {
        T Resolve<T>() where T : class;
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
        void ConfigureRequestPipeline(IApplicationBuilder application);
    }
}
