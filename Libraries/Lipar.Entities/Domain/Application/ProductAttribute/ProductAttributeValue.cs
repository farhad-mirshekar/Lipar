using System;

namespace Lipar.Entities.Domain.Application
{
   public class ProductAttributeValue : BaseEntity<Guid>
    {
        /// <summary>
        /// gets or sets the name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets or sets the price
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// gets or sets the ispreselected
        /// </summary>
        public bool IsPreSelected { get; set; }
        /// <summary>
        /// gets or sets product attribute mapping
        /// </summary>
        public Guid ProductAttributeMappingId { get; set; }

        #region Navigation
        public ProductAttributeMapping ProductAttributeMapping { get; set; }
        #endregion
    }
}
