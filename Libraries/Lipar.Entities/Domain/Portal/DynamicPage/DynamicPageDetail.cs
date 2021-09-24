using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;

namespace Lipar.Entities.Domain.Portal
{
   public class DynamicPageDetail : BaseEntity
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
        public int DynamicPageId { get; set; }
        public int UserId { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public User User { get; set; }
        public User Remover { get; set; }
        public DynamicPage DynamicPage { get; set; }
        public ViewStatus ViewStatus { get; set; }
        #endregion
    }
}
