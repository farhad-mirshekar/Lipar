using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
   public class City : BaseEntity<int>
    {
        public City()
        {
            UserAddresses = new HashSet<UserAddress>();
        }
        public string Title { get; set; }
        public int ProvinceId { get; set; }

        public Province Province { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }
    }
}
