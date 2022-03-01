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

            endpointRouteBuilder.MapControllerRoute("AddProductToCompare", "CompareProduct/add/{ProductId:guid}",
                new { controller = "Product", action = "AddProductInCompareList" });

            endpointRouteBuilder.MapControllerRoute("ClearCompareList", $"{pattern}CompareProduct/ClearAll",
                new { controller = "Product", action = "ClearCompareList" });

            endpointRouteBuilder.MapControllerRoute("RemoveProductFromCompareList", "CompareProduct/remove/{ProductId:guid}",
               new { controller = "Product", action = "RemoveProductFromCompareList" });

            endpointRouteBuilder.MapControllerRoute("AddProductComment", "Product/CreateComment",
                new { controller = "Product", action = "ProductCommentAdd" });

            endpointRouteBuilder.MapControllerRoute("AddProductQuestion", "Product/CreateQuestion",
                new { controller = "Product", action = "ProductQuestionAdd" });

            endpointRouteBuilder.MapControllerRoute("AddToCart", "Cart/Create/{ProductId:guid}",
                new { controller = "ShoppingCartItem", action = "ShoppingCartItemCreate" });

            endpointRouteBuilder.MapControllerRoute("ShoppingCartView", "Cart",
                new { controller = "ShoppingCartItem", action = "Cart" });
            
            endpointRouteBuilder.MapControllerRoute("ShoppingCartQuantityPlus", "Cart/QuantityPlus/{Id:guid}",
                new { controller = "ShoppingCartItem", action = "CartItemPlus" });

            endpointRouteBuilder.MapControllerRoute("ShoppingCartQuantityMinus", "Cart/QuantityMinus/{Id:guid}",
               new { controller = "ShoppingCartItem", action = "CartItemMinus" });

            endpointRouteBuilder.MapControllerRoute("ChangeLanguage", "ChangeLanguage/{LanguageId:guid}",
                new { controller = "Common", action = "SetLanguage" });

            endpointRouteBuilder.MapControllerRoute("Register", $"{pattern}Register/"
               , new { controller = "Account", action = "Register" });

            endpointRouteBuilder.MapControllerRoute("Checkout", $"Cart/Checkout",
                new { controller = "ShoppingCartItem", action = "Checkout" });

            endpointRouteBuilder.MapControllerRoute("UserAddressCreate", $"Cart/UserAddress/Create",
                new { controller = "ShoppingCartItem", action = "UserAddressCreate" });

            endpointRouteBuilder.MapControllerRoute("AddUserAddress", $"Cart/UserAddress/Create",
                new { controller = "ShoppingCartItem", action = "UserAddressCreate" });

            endpointRouteBuilder.MapControllerRoute("UserAddressEdit", "Cart/UserAddress/Edit/{Id:guid}",
                new { controller = "ShoppingCartItem", action = "UserAddressEdit" });

            endpointRouteBuilder.MapControllerRoute("UserAddressDelete", "Cart/UserAddress/Delete/{Id:guid}",
                 new { controller = "ShoppingCartItem", action = "UserAddressDelete" });

            endpointRouteBuilder.MapControllerRoute("PasswordRecovery", "Password-Recovery",
                new { controller = "Account", action = "PasswordRecovery" });

            endpointRouteBuilder.MapControllerRoute("PasswordRecoveryConfirm", "Password-Recovery-Confirm",
                new {controller="Account" , action= "PasswordRecoveryConfirm" });

            endpointRouteBuilder.MapControllerRoute("AddProductAnswers", "Product/CreateProductAnswers",
                new { controller = "Product", action = "ProductAnswerAdd" });
        }
        #endregion
    }
}
