using Lipar.Entities.Domain.Organization;

namespace Lipar.Entities.Domain.General
{
    public class LocaleStringResource : BaseEntity
    {
        #region Fields
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public int LanguageId { get; set; }
        public int UserId { get; set; }
        #endregion

        #region Naviation
        public Language Language { get; set; }
        public User User { get; set; }
        #endregion
    }
}
