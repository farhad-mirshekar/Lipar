using System;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// product tag mapping
    /// </summary>
    public class ProductTagMapping : BaseEntity<Guid>
    {
        #region Fields
        /// <summary>
        /// gets or sets the product tag
        /// </summary>
        public Guid ProductTagId { get; set; }

        /// <summary>
        /// gets or sets the product
        /// </summary>
        public Guid ProductId { get; set; }
        #endregion

        #region Navigations
        public ProductTag ProductTag { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
