using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using System;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class UserPasswordSeed : BaseSeed<UserPassword>
    {
        public UserPasswordSeed()
        {
            Items = new List<UserPassword>()
            {
                new UserPassword()
                {
                    Id = Guid.Parse("5AB96467-C5AB-4C6A-A541-0336B4AEFBE4"),
                    Password="123",
                    PasswordFormatTypeId = (int)PasswordFormatTypeEnum.Clear,
                    UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    PasswordSalt=string.Empty,
                }
            };
        }
        public override string ModelName => "UserPassword";

        public override string Schema => "Organization";

        public override List<UserPassword> Items { get; set; }
    }
}
