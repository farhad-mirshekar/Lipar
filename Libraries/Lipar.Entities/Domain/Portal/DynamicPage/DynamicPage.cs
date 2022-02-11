using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    public class DynamicPage : BaseEntity<Guid>
    {
        #region Ctor
        public DynamicPage()
        {
            DynamicPageDetails = new HashSet<DynamicPageDetail>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public int ViewStatusId { get; set; }
        public Guid UserId { get; set; }
        public int? LanguageId { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public User User { get; set; }
        public User Remover { get; set; }
        public ViewStatus ViewStatus { get; set; }
        public ICollection<DynamicPageDetail> DynamicPageDetails { get; set; }
        #endregion
    }
}
