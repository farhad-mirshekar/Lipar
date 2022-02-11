

using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
   public class Province : BaseEntity<int>
    {
        public Province()
        {
            Cities = new HashSet<City>();
            UserAddresses = new HashSet<UserAddress>();
        }
        public string Title { get; set; }
        public int CountryId { get; set; }


        public Country Country { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
