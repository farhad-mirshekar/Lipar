using Lipar.Core.Infrastructure;
using Lipar.Web.Framework.Infrastructure.Extensions;
using Lipar.Web.Framework.MVC.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using FluentValidation.AspNetCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Lipar.Web.Framework.Infrastructure
{
    public class MvcStartup : ILiparStartup
    {
        public int Order => 1000;

        public void Configure(IApplicationBuilder application)
        {
            application.LiparStaticFiles();
            application.UseLiparEndpoints();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddControllersWithViews();

            services.AddTransient<GenericPathRoute>();
            services.AddRazorPages();

            //services.AddKendo();

            //MVC now serializes JSON with camel case names by default, use this code to avoid it
            mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            mvcBuilder.AddFluentValidation(configuration=> 
            {
                var assemblies = mvcBuilder.PartManager.ApplicationParts
                .OfType<AssemblyPart>()
                .Where(part => part.Name.StartsWith("Lipar", System.StringComparison.InvariantCultureIgnoreCase))
                .Select(part => part.Assembly);

                configuration.RegisterValidatorsFromAssemblies(assemblies);

                configuration.ImplicitlyValidateChildProperties = true;
            });
        }
    }
}
