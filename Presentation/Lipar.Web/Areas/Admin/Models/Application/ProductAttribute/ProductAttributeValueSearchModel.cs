using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductAttributeValueSearchModel : BaseSearchModel
    {
        public Guid ProductAttributeMappingId { get; set; }
    }
}
