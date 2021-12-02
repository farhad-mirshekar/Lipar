namespace Lipar.Web.Areas.Dashboard.Models.Organization
{
    public class ProfileModel : BaseEntityModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string NationalCode { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
    }
}
