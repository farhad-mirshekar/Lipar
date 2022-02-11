using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Core
{
   public class Gender : BaseEntity<int>
    {
        public Gender()
        {
            Users = new HashSet<User>();
        }
        public string Title { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
