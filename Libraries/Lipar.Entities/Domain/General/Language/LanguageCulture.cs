using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
   public class LanguageCulture : BaseEntity
    {
        #region Ctor
        public LanguageCulture()
        {
            Languages = new HashSet<Language>();
        }
        #endregion

        #region
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// gets or sets the seo
        /// </summary>
        public string Seo { get; set; }
        #endregion

        #region Navigations
        public ICollection<Language> Languages { get; set; }
        #endregion
    }
}
