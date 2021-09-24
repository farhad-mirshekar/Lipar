using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class PositionTypeSeed : BaseSeed<PositionType>
    {
        public PositionTypeSeed()
        {
            Items = new List<PositionType>()
            {
                new PositionType { Id = 1, Title ="مدیرسایت" },
            };
        }
        public override string ModelName => "PositionType";

        public override string Schema => "Organization";

        public override List<PositionType> Items { get; set; }
    }
}
