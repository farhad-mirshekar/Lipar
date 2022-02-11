using System;

namespace Lipar.Web.Models.Portal
{
    public class DynamicPageModel : BaseEntityModel<Guid>
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
