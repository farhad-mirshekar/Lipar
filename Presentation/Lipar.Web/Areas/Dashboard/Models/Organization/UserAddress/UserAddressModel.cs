using Lipar.Web.Framework.Models;

namespace Lipar.Web.Areas.Dashboard.Models.Organization
{
    public class UserAddressModel : BaseEntityModel
    {
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
    }
}
