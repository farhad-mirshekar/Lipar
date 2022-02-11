using System;

namespace Lipar.Web.Models.Organization
{
    public class UserAddressModel : BaseEntityModel<Guid>
    {
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
    }
}
