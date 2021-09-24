using Lipar.Web.Framework.MVC.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Lipar.Web.Infrastructure
{
    public class GenericUrlRouteProvider : IRouteProvider
    {
        public int Priority => -1000000;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var pattern = "{EntityName}/{SeName}";

            endpointRouteBuilder.MapDynamicControllerRoute<GenericPathRoute>(pattern);

            endpointRouteBuilder.MapControllerRoute(
              name: "Default",
              pattern: "{controller=Home}/{action=Index}/{id?}");

            //generic URLs

            endpointRouteBuilder.MapControllerRoute(
              name: "GenericUrl",
              pattern: "{GenericEntityName}/{GenericSeName}",
              new { controller = "Common", action = "GenericUrl" });

            endpointRouteBuilder.MapControllerRoute("BlogDetail", pattern,
                new { controller = "Blog", action = "Detail" });

            endpointRouteBuilder.MapControllerRoute("StaticPageDetail", pattern,
                new { controller = "StaticPage", action = "Detail" });

            endpointRouteBuilder.MapControllerRoute("DynamicPageDetail", pattern,
                new { controller = "DynamicPage", action = "Detail" });

            endpointRouteBuilder.MapControllerRoute("DynamicPageDetails", pattern,
                new { controller = "DynamicPage", action = "DetailPage" });

            endpointRouteBuilder.MapControllerRoute("ProductDetail", pattern,
                new { controller = "Product", action = "Detail" });
        }
    }
}
