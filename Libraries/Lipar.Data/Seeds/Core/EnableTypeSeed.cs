using Lipar.Entities.Domain.Core;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Core
{
    public class EnableTypeSeed : BaseSeed<EnabledType>
    {
        public EnableTypeSeed()
        {
            Items = new List<EnabledType>()
            {
                new EnabledType { Id=1, Title ="فعال"},
                new EnabledType { Id=2, Title ="غیرفعال"},
            };
        }
        public override string ModelName => "EnableType";

        public override string Schema => "Core";

        public override List<EnabledType> Items { get; set; }
    }
}
