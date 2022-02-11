using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductAttributeMappingSearchModel : BaseSearchModel
    {
        public Guid ProductId { get; set; }
    }
}
