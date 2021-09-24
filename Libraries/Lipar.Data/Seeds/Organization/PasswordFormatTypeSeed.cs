using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class PasswordFormatTypeSeed : BaseSeed<PasswordFormatType>
    {
        public PasswordFormatTypeSeed()
        {
            Items = new List<PasswordFormatType>()
            {
                new PasswordFormatType { Id = 1, Title="Clear"},
                new PasswordFormatType { Id = 2, Title="Hashed"},
                new PasswordFormatType { Id = 3, Title="Encrypted"},
            };
        }
        public override string ModelName => "PasswordFormatType";

        public override string Schema => "Organization";

        public override List<PasswordFormatType> Items { get; set; }
    }
}
