using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
   public class AttributeControlType:BaseEntity<int>
    {
        #region Ctor
        public AttributeControlType()
        {
            ProductAttributeMappings = new HashSet<ProductAttributeMapping>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; }
        #endregion
    }
}
