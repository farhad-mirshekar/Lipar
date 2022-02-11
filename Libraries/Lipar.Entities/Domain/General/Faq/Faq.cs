using Lipar.Entities.Domain.Organization;
using System;

namespace Lipar.Entities.Domain.General
{
    public class Faq : BaseEntity<Guid>
    {
        #region Ctor
        public Guid FaqGroupId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        #endregion

        #region Navigations
        public FaqGroup FaqGroup { get; set; }
        #endregion
    }
}
