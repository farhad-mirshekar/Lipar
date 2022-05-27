using System;

namespace Lipar.Entities.Domain.Application
{
    /// <summary>
    /// order detail attribute
    /// </summary>
    public class OrderDetailAttribute : BaseEntity<Guid>
    {
        /// <summary>
        /// gets or sets the order detail
        /// </summary>
        public Guid OrderDetailId { get; set; }

        /// <summary>
        /// gets or sets product attrivute value
        /// </summary>
        public Guid ProductAttributeValueId { get; set; }

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

        #region Navigations
        public OrderDetail OrderDetail { get; set; }
        #endregion
    }
}
