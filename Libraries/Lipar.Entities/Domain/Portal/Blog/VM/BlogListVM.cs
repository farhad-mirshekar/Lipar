using System;

namespace Lipar.Entities.Domain.Portal
{
    public class BlogListVM : BaseListVM
    {
        public string Name { get; set; }
        public Guid? UserId { get; set; }
    }
}
