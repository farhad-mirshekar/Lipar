using System;

namespace Lipar.Entities.Domain.General
{
    public class LocaleStringResource : BaseEntity<Guid>
    {
        #region Fields
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public int LanguageId { get; set; }
        #endregion

        #region Naviation
        public Language Language { get; set; }
        #endregion
    }
}
