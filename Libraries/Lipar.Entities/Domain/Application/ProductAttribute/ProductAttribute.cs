using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// product attribute
    /// </summary>
   public class ProductAttribute : BaseEntity<Guid>
    {
        #region Ctor
        public ProductAttribute()
        {
            ProductAttributeMappings = new HashSet<ProductAttributeMapping>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the name attribute
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets or sets the description attribute
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Navigations
        public ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; }
        #endregion
    }
}
