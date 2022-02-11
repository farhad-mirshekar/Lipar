using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// product_media_mapping
    /// </summary>
   public class ProductMedia : BaseEntity<Guid>
    {
        /// <summary>
        /// gets or sets the product id
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// gets or sets the media id
        /// </summary>
        public Guid MediaId { get; set; }
        /// <summary>
        /// gets or sets the priority
        /// </summary>
        public int Priority { get; set; }

        #region Navigations
        public Product Product { get; set; }
        public Media Media { get; set; }
        #endregion
    }
}
