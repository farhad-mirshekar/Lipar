using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
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
                    Id = 1,
                    Address="البرز",
                    CenterId = 1,
                    CodePhone="026",
                    DepartmentTypeId = (int)DepartmentTypeEnum.Main_Unit,
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
