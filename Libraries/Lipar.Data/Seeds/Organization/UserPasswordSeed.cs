using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
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
                    Id = 1,
                    Password="123",
                    PasswordFormatTypeId = (int)PasswordFormatTypeEnum.Clear,
                    UserId = 1,
                    PasswordSalt=string.Empty,
                }
            };
        }
        public override string ModelName => "UserPassword";

        public override string Schema => "Organization";

        public override List<UserPassword> Items { get; set; }
    }
}
