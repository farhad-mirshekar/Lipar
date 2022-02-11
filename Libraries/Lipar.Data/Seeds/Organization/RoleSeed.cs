using Lipar.Entities.Domain.Organization;
using System;
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
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CenterId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name="مدیر سایت",
                }
            };
        }
        public override string ModelName => "Role";

        public override string Schema => "Organization";

        public override List<Role> Items { get; set; }
    }
}
