using Lipar.Entities.Domain.Application;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Application
{
    public class DiscountTypeSeed : BaseSeed<DiscountType>
    {
        public DiscountTypeSeed()
        {
            Items = new List<DiscountType>()
            {
                new DiscountType(){Id = 1, Title ="مبلغی"},
                new DiscountType(){Id = 2, Title ="درصدی"},
            };
        }
        public override string ModelName => "DiscountType";

        public override string Schema => "Application";

        public override List<DiscountType> Items { get; set; }
    }
}
