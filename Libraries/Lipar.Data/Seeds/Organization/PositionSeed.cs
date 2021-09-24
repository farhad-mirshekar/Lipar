using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class PositionSeed : BaseSeed<Position>
    {
        public PositionSeed()
        {
            Items = new List<Position>()
            {
                new Position()
                {
                    Id = 1,
                    CenterId = 1,
                    Default = true,
                    DepartmentId = 1,
                    EnabledTypeId = (int)EnabledTypeEnum.Active,
                    PositionTypeId = (int)PositionTypeEnum.Managment,
                    UserId= 1,
                }
            };
        }
        public override string ModelName => "Position";

        public override string Schema => "Organization";

        public override List<Position> Items { get; set; }
    }
}
