using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
   public class Category : BaseEntity<Guid>
    {
        #region Ctor
        public Category()
        {
            Children = new HashSet<Category>();
            Products = new HashSet<Product>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public Guid? ParentId { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public int EnabledTypeId { get; set; }
        public Guid UserId { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public Category Parent { get; set; }
        public EnabledType EnabledType { get; set; }
        public User User { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Category> Children { get; set; }
        #endregion
    }
}
