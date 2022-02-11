using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.General;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Organization
{
    public class UserAddress : BaseEntity<Guid>
    {
        public UserAddress()
        {
            Orders = new HashSet<Order>();
        }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public Guid UserId { get; set; }

        //navigations
        public User User { get; set; }
        public Country Country { get; set; }
        public Province Province { get; set; }
        public City City { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
