using Lipar.Entities;
using System;

namespace Lipar.Web.Models.Application
{
    public class CategoryModel : BaseEntityModel<Guid>
    {
        public string Name { get; set; }
        public string NameCrumb { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public Guid? ParentId { get; set; }
        public int EnabledTypeId { get; set; }
        public bool IncludeInTopMenu { get; set; }
    }
}
