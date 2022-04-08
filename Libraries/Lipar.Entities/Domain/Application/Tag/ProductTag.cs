using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// product tag
    /// </summary>
    public class ProductTag : BaseEntity<Guid>
    {
        #region Ctor
        public ProductTag()
        {
            ProductTagMappings = new HashSet<ProductTagMapping>();
        }
        #endregion

        #region Fields

        /// <summary>
        /// gets or sets the name
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Navigations
        public ICollection<ProductTagMapping> ProductTagMappings { get; set; }
        #endregion
    }
}
