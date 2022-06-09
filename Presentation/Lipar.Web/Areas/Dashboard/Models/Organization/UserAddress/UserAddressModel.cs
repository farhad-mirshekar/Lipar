using System;

namespace Lipar.Web.Areas.Dashboard.Models.Organization
{
    public class UserAddressModel : BaseEntityModel
    {
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
    }
}
