using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    public class FaqGroup : BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }

        #region Navigations
        public User User { get; set; }
        public User Remover { get; set; }
        public ICollection<Faq> Faqs { get; set; }
        #endregion
    }
}
