using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
    public class PasswordFormatType : BaseEntity<int>
    {
        #region Ctor
        public PasswordFormatType()
        {
            UserPasswords = new HashSet<UserPassword>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<UserPassword> UserPasswords { get; set; }
        #endregion
    }
}
