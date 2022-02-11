using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;

namespace Lipar.Entities.Domain.Portal
{
   public class StaticPage : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public int Priority { get; set; }
        public int ViewStatusId { get; set; }
        public Guid UserId { get; set; }
        public int? LanguageId { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }

        #region Navigations
        public User User { get; set; }
        public User Remover { get; set; }
        public ViewStatus ViewStatus { get; set; }
        #endregion
    }
}
