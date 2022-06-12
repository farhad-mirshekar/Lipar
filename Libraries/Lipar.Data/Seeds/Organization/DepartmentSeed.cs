using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using System;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class DepartmentSeed : BaseSeed<Department>
    {
        public DepartmentSeed()
        {
            Items = new List<Department>()
            {
                new Department()
                {
                    Id = Guid.Parse("97AA0260-4810-4356-9979-1515DBDFD5BB"),
                    Address="البرز",
                    CenterId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CodePhone="026",
                    DepartmentTypeId = (int)DepartmentTypeEnum.MainUnit,
                    EnabledTypeId = (int)EnabledTypeEnum.Active,
                    Name="واحد مرکزی",
                    Phone="33309282",
                    PostalCode="234324",
                }
            };
        }
        public override string ModelName => "Department";

        public override string Schema => "Organization";

        public override List<Department> Items { get; set; }
    }
}
