using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class CommandTypeSeed : BaseSeed<CommandType>
    {
        public CommandTypeSeed()
        {
            Items = new List<CommandType>()
            {
                new CommandType { Id = 1, Title ="Application"},
                new CommandType { Id = 2, Title = "Menu"},
                new CommandType { Id = 3, Title="Pages" }
            };
        }
        public override string ModelName => "CommandType";

        public override string Schema => "Organization";

        public override List<CommandType> Items { get; set; }
    }
}
