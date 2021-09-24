namespace Lipar.Entities.Domain.General
{
    /// <summary>
    /// contact us 
    /// </summary>
   public class ContactUs : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int ContactUsTypeId { get; set; }
        public string Body { get; set; }

        #region Navigations
        public ContactUsType contactUsType { get; set; }
        #endregion
    }
}
