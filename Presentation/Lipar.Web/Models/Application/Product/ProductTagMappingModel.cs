using System;

namespace Lipar.Web.Models.Application
{
    public class ProductTagMappingModel : BaseEntityModel<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid ProductTagId { get; set; }
        public string Name { get; set; }
    }
}
