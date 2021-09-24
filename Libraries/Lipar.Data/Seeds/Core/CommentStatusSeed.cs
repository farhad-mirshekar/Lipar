using Lipar.Entities.Domain.Core;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Core
{
    public class CommentStatusSeed : BaseSeed<CommentStatus>
    {
        public CommentStatusSeed()
        {
            Items = new List<CommentStatus>()
            {
                new CommentStatus { Id = 1, Title="فعال"},
                new CommentStatus { Id = 2, Title="غیرفعال"},
            };
        }
        public override string ModelName => "CommentStatus";

        public override string Schema => "Core";

        public override List<CommentStatus> Items { get; set; }
    }
}
