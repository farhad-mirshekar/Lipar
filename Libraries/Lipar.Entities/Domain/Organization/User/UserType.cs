using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
   public class UserType : BaseEntity
    {
        #region Ctor
        public UserType()
        {
            Users = new HashSet<User>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<User> Users { get; set; }
        #endregion
    }
}
