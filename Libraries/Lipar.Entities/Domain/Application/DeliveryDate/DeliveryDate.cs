using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
   public class DeliveryDate : BaseEntity
    {
        #region Ctor
        public DeliveryDate()
        {
            Products = new HashSet<Product>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Description { get; set; }
        public int EnabledTypeId { get; set; }
        public int Priority { get; set; }
        public int UserId { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public User User { get; set; }
        public EnabledType EnabledType { get; set; }
        public ICollection<Product> Products { get; set; }
        #endregion
    }
}
