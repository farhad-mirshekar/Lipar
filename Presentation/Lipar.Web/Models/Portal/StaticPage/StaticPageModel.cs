using System;

namespace Lipar.Web.Models.Portal
{
    public class StaticPageModel : BaseEntityModel<Guid>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public int Priority { get; set; }
        public int ViewStatusId { get; set; }
    }
}
