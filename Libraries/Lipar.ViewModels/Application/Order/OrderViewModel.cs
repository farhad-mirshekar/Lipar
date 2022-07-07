using System;
using System.Collections.Generic;

namespace Lipar.ViewModels.Application
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderDetails = new List<OrderDetailViewModel>();
        }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public Guid UserAddressId { get; set; }
        public Guid BankPortId { get; set; }
        public string BankName { get; set; }
        public decimal? ShoppingCartRate { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }

        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
