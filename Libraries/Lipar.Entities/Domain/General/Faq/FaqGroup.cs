using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    public class FaqGroup : BaseEntity<Guid>
    {
        public string Name { get; set; }

        #region Navigations
        public ICollection<Faq> Faqs { get; set; }
        #endregion
    }
}
