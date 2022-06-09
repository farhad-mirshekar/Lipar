using Lipar.Web.Framework.Models;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Dashboard.Models.Application
{
    public class OrderListModel
    {
        public OrderListModel()
        {
            AvailableOrders = new List<OrderModel>();
        }
        public IList<OrderModel> AvailableOrders { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}
