using Lipar.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductAttributeMappingModel : BaseEntityModel
    {
        #region Ctor
        public ProductAttributeMappingModel()
        {
            AvailableAttributeControlType = new List<SelectListItem>();
            AvailableAttributes = new List<SelectListItem>();
            ProductAttributeValueSearchModel = new ProductAttributeValueSearchModel();
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

        #region Select
        public IList<SelectListItem> AvailableAttributeControlType { get; set; }
        public IList<SelectListItem> AvailableAttributes { get; set; }
        #endregion

        #region Utilities
        public ProductAttributeValueSearchModel ProductAttributeValueSearchModel { get; set; }
        #endregion
    }
}
