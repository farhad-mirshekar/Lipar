using System;

namespace Lipar.Entities.Domain.Application
{
   public class ProductAttributeMappingListVM : BaseListVM
    {
        public Guid? ProductId { get; set; }
        public Guid? AttributeId { get; set; }
    }
}
