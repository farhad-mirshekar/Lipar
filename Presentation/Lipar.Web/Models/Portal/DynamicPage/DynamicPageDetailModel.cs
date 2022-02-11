using System;

namespace Lipar.Web.Models.Portal
{
    public class DynamicPageDetailModel : BaseEntityModel<Guid>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public int Priority { get; set; }

        public Guid DynamicPageId { get; set; }
        public string DynamicPageName { get; set; }
    }
}
