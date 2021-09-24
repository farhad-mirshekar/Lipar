using Lipar.Web.Framework.Models;
using System.Collections.Generic;

namespace Lipar.Web.Models.Application
{
    public class ProductListModel
    {
        public ProductListModel()
        {
            AvailableProducts = new List<ProductModel>();
            PagingInfo = new PagingInfo();
        }
        public IEnumerable<ProductModel> AvailableProducts { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
