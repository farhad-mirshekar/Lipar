namespace Lipar.Web.Models.Organization
{
    public class UserAddressModel : BaseEntityModel
    {
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
    }
}
