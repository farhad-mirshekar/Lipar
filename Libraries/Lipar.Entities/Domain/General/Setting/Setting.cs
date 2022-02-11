using System;

namespace Lipar.Entities.Domain.General
{
   public class Setting : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
