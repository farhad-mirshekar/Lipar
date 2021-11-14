using System.Collections.Generic;

namespace Lipar.Web.Models.Application
{
    public class ShoppingCartItemListModel
    {
        public ShoppingCartItemListModel()
        {
            AvailableShoppingCartItemModels = new List<ShoppingCartItemModel>();
        }
        public IList<ShoppingCartItemModel> AvailableShoppingCartItemModels { get; set; }
        public decimal CartAmount { get; set; }
    }
}
