using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Portal
{
    public class DynamicPageDetailSearchModel : BaseSearchModel
    {
        public Guid? DynamicPageId { get; set; }
    }
}
