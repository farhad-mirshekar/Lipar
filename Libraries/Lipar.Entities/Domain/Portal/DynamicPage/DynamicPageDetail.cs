using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;

namespace Lipar.Entities.Domain.Portal
{
   public class DynamicPageDetail : BaseEntity<Guid>
    {
        #region Fields
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public int Priority { get; set; }
        public int ViewStatusId { get; set; }
        public Guid DynamicPageId { get; set; }
        public Guid UserId { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public User User { get; set; }
        public DynamicPage DynamicPage { get; set; }
        public ViewStatus ViewStatus { get; set; }
        #endregion
    }
}
