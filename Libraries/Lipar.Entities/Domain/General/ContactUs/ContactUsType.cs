using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
   public class ContactUsType : BaseEntity<int>
    {
        #region Ctor
        public ContactUsType()
        {
            ContactUs = new HashSet<ContactUs>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Navigations
        public ICollection<ContactUs> ContactUs { get; set; }
        #endregion
    }
}
