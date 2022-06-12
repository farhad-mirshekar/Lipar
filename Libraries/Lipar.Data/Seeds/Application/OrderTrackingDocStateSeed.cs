using Lipar.Entities.Domain.Application;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Application
{
    internal class OrderTrackingDocStateSeed : BaseSeed<OrderTrackingDocState>
    {
        public OrderTrackingDocStateSeed()
        {
            Items = new List<OrderTrackingDocState>
            {
                new OrderTrackingDocState{Id = 1 , Title = "ثبت اولیه"},
                new OrderTrackingDocState{Id = 2 , Title = "بررسی مالی"},
                new OrderTrackingDocState{Id = 3 , Title = "بررسی واحد انبار و لجستیک"},
                new OrderTrackingDocState{Id = 4 , Title = "ارسال محصول"},
                new OrderTrackingDocState{Id = 5 , Title = "تحویل به مشتری"},
            };
        }
        public override string ModelName => "OrderTrackingDocState";

        public override string Schema => "Application";

        public override List<OrderTrackingDocState> Items { get; set; }
    }
}
