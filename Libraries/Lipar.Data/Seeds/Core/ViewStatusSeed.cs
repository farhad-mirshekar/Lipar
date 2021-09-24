using Lipar.Entities.Domain.Core;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Core
{
    public class ViewStatusSeed : BaseSeed<ViewStatus>
    {
        public ViewStatusSeed()
        {
            Items = new List<ViewStatus>();

            Items.Add(new ViewStatus { Id = 1, Title = "فعال" });
            Items.Add(new ViewStatus { Id = 2, Title = "غیرفعال" });
        }
        public override string ModelName => "ViewStatus";

        public override string Schema => "Core";

        public override List<ViewStatus> Items { get; set; }
    }
}
