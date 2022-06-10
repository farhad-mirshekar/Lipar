using System;

namespace Lipar.Web.Areas.Dashboard.Models.Application
{
    public class OrderDetailAttributeModel:BaseEntityModel
    {
        public Guid OrderDetailId { get; set; }

        public Guid ProductAttributeValueId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool IsPreSelected { get; set; }
        public Guid ProductAttributeMappingId { get; set; }
        public string AttributeName { get; set; }
    }
}
