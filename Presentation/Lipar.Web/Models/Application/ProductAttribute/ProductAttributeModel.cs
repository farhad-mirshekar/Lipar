using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.Application
{
    public class ProductAttributeModel : BaseEntityModel<Guid>
    {
        public ProductAttributeModel()
        {
            ProductAttributeValues = new List<ProductAttributeValueModel>();
        }
        public string TextPrompt { get; set; }
        public int AttributeControlTypeId { get; set; }
        public bool IsRequired { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductAttributeId { get; set; }
        public IList<ProductAttributeValueModel> ProductAttributeValues { get; set; }
    }
}
