using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    public class Country : BaseEntity<int>
    {
        public Country()
        {
            Provinces = new HashSet<Province>();
            UserAddresses = new HashSet<UserAddress>();
        }
        public string Title { get; set; }

        public ICollection<Province> Provinces { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }
    }
}
