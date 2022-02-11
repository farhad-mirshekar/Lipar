using System;

namespace Lipar.Entities.Domain.Application
{
   public class ProductMediaListVM : BaseListVM
    {
        public Guid? ProductId { get; set; }
        public Guid? MediaId { get; set; }
    }
}
