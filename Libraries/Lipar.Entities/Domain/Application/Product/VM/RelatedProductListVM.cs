using System;

namespace Lipar.Entities.Domain.Application
{
   public class RelatedProductListVM : BaseListVM
    {
        public Guid? ProductId1 { get; set; }
        /// <summary>
        /// no show for popup
        /// </summary>
        public bool? Hide { get; set; }
        public string Name { get; set; }
    }
}
