using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
   public class ProductAttributeMapping : BaseEntity
    {
        #region Ctor
        public ProductAttributeMapping()
        {
            ProductAttributeValues = new HashSet<ProductAttributeValue>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the text prompt
        /// </summary>
        public string TextPrompt { get; set; }
        /// <summary>
        /// gets or sets the attribute control type
        /// </summary>
        public int AttributeControlTypeId { get; set; }
        /// <summary>
        /// gets or sets the is required
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// gets or sets the product
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// gets or sets the product attribute
        /// </summary>
        public int ProductAttributeId { get; set; }
        #endregion

        #region Navigations
        public Product Product { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
        public AttributeControlType AttributeControlType { get; set; }
        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        #endregion
    }
}
