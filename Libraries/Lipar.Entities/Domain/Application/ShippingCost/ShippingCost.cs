using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
   public class ShippingCost : BaseEntity<Guid>
    {
        #region Ctor
        public ShippingCost()
        {
            Products = new HashSet<Product>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int EnabledTypeId { get; set; }
        public int Priority { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public EnabledType EnabledType { get; set; }
        public ICollection<Product> Products { get; set; }
        #endregion
    }
}
