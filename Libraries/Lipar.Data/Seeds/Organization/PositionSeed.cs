using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using System;
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
                    Id = Guid.Parse("6B7494B9-F417-4434-A306-E45DD0EF0833"),
                    CenterId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Default = true,
                    DepartmentId = Guid.Parse("97AA0260-4810-4356-9979-1515DBDFD5BB"),
                    EnabledTypeId = (int)EnabledTypeEnum.Active,
                    PositionTypeId = (int)PositionTypeEnum.Managment,
                    UserId= Guid.Parse("11111111-1111-1111-1111-111111111111"),
                }
            };
        }
        public override string ModelName => "Position";

        public override string Schema => "Organization";

        public override List<Position> Items { get; set; }
    }
}
