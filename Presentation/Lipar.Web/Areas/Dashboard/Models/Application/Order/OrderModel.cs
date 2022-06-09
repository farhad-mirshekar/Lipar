using Lipar.Web.Areas.Dashboard.Models.Organization;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Dashboard.Models.Application
{
    public class OrderModel : BaseEntityModel
    {
        public OrderModel()
        {
            AvailableOrderDetails = new List<OrderDetailModel>();
            CustomerInfo = new ProfileModel();
        }
        public decimal Price { get; set; }
        public string BankName { get; set; }
        public ProfileModel CustomerInfo { get; set; }
        public IEnumerable<OrderDetailModel> AvailableOrderDetails { get; set; }
    }
}
