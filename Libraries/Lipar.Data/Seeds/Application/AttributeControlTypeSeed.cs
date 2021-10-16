using Lipar.Entities.Domain.Application;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Application
{
    public class AttributeControlTypeSeed : BaseSeed<AttributeControlType>
    {
        public AttributeControlTypeSeed()
        {
            Items = new List<AttributeControlType>()
            {
                new AttributeControlType() { Id = 1, Title ="Dropdown"}
            };
        }
        public override string ModelName => "AttributeControlType";

        public override string Schema => "Application";

        public override List<AttributeControlType> Items { get; set; }
    }
}
