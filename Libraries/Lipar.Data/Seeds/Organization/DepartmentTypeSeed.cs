using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class DepartmentTypeSeed : BaseSeed<DepartmentType>
    {
        public DepartmentTypeSeed()
        {
            Items = new List<DepartmentType>()
            {
                new DepartmentType { Id = 1, Title="سامانه اصلی"},
                new DepartmentType { Id = 2, Title="واحد فروش"},
                new DepartmentType { Id = 3, Title="واحد مالی"},
                new DepartmentType { Id = 4, Title="واحد انبار"},
            };
        }
        public override string ModelName => "DepartmentType";

        public override string Schema => "Organization";

        public override List<DepartmentType> Items { get; set; }
    }
}
