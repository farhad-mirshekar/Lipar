using System;

namespace Lipar.Entities.Domain.Portal
{
    public class BlogMediaListVM : BaseListVM
    {
        public Guid? BlogId { get; set; }
        public Guid? MediaId { get; set; }
    }
}
