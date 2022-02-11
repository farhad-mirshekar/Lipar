using Lipar.Entities;
using System;

namespace Lipar.Web.Models.Application
{
    public class DeliveryDateModel : BaseEntityModel<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EnabledTypeId { get; set; }
        public int Priority { get; set; }
    }
}
