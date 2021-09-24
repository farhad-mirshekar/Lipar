using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class CenterSeed : BaseSeed<Center>
    {
        public CenterSeed()
        {
            Items = new List<Center>();
            Items.Add(new Center
            {
                Id = 1,
                Code ="1000",
                CreationDate=DateTime.Now,
                EnabledTypeId = (int)EnabledTypeEnum.Active,
                Name="لیپار",
            });
        }
        public override string ModelName => "Center";

        public override string Schema => "Organization";

        public override List<Center> Items { get; set; }

    }
}
