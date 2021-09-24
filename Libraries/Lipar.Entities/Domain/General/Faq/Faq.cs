using Lipar.Entities.Domain.Organization;

namespace Lipar.Entities.Domain.General
{
    public class Faq : BaseEntity
    {
        #region Ctor
        public int FaqGroupId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int UserId { get; set; }
        #endregion

        #region Navigations
        public FaqGroup FaqGroup { get; set; }
        public User User { get; set; }
        #endregion
    }
}
