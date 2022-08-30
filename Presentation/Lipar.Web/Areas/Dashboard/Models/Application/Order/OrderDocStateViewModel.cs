using Lipar.ViewModels.Application.Order;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Dashboard.Models.Application
{
    public class OrderDocStateViewModel
    {
        public OrderDocStateViewModel()
        {
            AvailableDocStates = new Dictionary<int, string>();
            AvailableOrderTrackingFlows = new List<OrderTrackingFlowViewModel>();
        }
        public Dictionary<int , string> AvailableDocStates { get; set; }
        public List<OrderTrackingFlowViewModel> AvailableOrderTrackingFlows { get; set; }
    }
}
