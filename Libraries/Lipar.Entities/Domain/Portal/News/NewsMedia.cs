using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Entities.Domain.Portal
{
    public class NewsMedia: BaseEntity<Guid>
    {
        public News News { get; set; }
        public Guid NewsId { get; set; }

        public Media Media { get; set; }
        public Guid MediaId { get; set; }
    }
}
