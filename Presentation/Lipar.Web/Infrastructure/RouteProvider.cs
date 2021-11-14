using Lipar.Web.Framework.MVC.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Lipar.Web.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        #region Properties
        public int Priority => 1000;
        #endregion

        #region Method
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var pattern = string.Empty;

            //areas
            endpointRouteBuilder.MapControllerRoute(name: "areaRoute",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            //home page
            endpointRouteBuilder.MapControllerRoute("Homepage", pattern,
                new { controller = "Home", action = "Index" });

            endpointRouteBuilder.MapControllerRoute("Login", $"{pattern}login/"
                , new { controller = "Account", action = "Login" });

            endpointRouteBuilder.MapControllerRoute("Blogs", $"{pattern}Blogs/" + "{page?}",
                new { controller = "Blog", action = "List" });

            endpointRouteBuilder.MapControllerRoute("ContactUs", "ContactUs",
                new { controller = "Common", action = "ContactUs" });

            endpointRouteBuilder.MapControllerRoute("CompareProducts", "CompareProducts",
                new { controller = "Product", action = "CompareProducts" });

            endpointRouteBuilder.MapControllerRoute("AddProductToCompare", "CompareProduct/add/{ProductId:min(0)}",
                new { controller = "Product", action = "AddProductInCompareList" });

            endpointRouteBuilder.MapControllerRoute("ClearCompareList", $"{pattern}CompareProduct/ClearAll",
                new { controller = "Product", action = "ClearCompareList" });

            endpointRouteBuilder.MapControllerRoute("RemoveProductFromCompareList", "CompareProduct/remove/{ProductId:min(0)}",
               new { controller = "Product", action = "RemoveProductFromCompareList" });

            endpointRouteBuilder.MapControllerRoute("AddProductComment", "Product/CreateComment",
                new { controller = "Product", action = "ProductCommentAdd" });

            endpointRouteBuilder.MapControllerRoute("AddProductQuestion", "Product/CreateQuestion",
                new { controller = "Product", action = "ProductQuestionAdd" });

            endpointRouteBuilder.MapControllerRoute("AddToCart", "Cart/Create/{ProductId:min(0)}",
                new { controller = "ShoppingCartItem", action = "ShoppingCartItemCreate" });

            endpointRouteBuilder.MapControllerRoute("ShoppingCartView", "Cart",
                new { controller = "ShoppingCartItem", action = "Cart" });
            
            endpointRouteBuilder.MapControllerRoute("ShoppingCartQuantityPlus", "Cart/QuantityPlus/{Id:min(0)}",
                new { controller = "ShoppingCartItem", action = "CartItemPlus" });

            endpointRouteBuilder.MapControllerRoute("ShoppingCartQuantityMinus", "Cart/QuantityMinus/{Id:min(0)}",
               new { controller = "ShoppingCartItem", action = "CartItemMinus" });

            endpointRouteBuilder.MapControllerRoute("ChangeLanguage", "ChangeLanguage/{LanguageId:min(0)}",
                new { controller = "Common", action = "SetLanguage" });
        }
        #endregion
    }
}
