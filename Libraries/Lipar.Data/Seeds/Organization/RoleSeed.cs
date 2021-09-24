using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class RoleSeed : BaseSeed<Role>
    {
        public RoleSeed()
        {
            Items = new List<Role>()
            {
                new Role()
                {
                    Id = 1,
                    CenterId = 1,
                    Name="مدیر سایت",
                }
            };
        }
        public override string ModelName => "Role";

        public override string Schema => "Organization";

        public override List<Role> Items { get; set; }
    }
}
