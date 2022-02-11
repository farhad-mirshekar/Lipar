using System;

namespace Lipar.Entities.Domain.Application
{
   public class ProductAttributeValueListVM:BaseListVM
    {
        public Guid? ProductAttributeMappingId { get; set; }
        public string Name { get; set; }
    }
}
