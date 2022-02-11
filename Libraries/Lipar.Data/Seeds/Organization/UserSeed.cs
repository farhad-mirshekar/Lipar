using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Organization.Enums;
using System;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class UserSeed : BaseSeed<User>
    {
        public UserSeed()
        {
            Items = new List<User>()
            {
                new User()
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CellPhone="33309282",
                    CellPhoneVerified = true,
                    Email="a@a.com",
                    EmailVerified = true,
                    EnabledTypeId = (int)EnabledTypeEnum.Active,
                    FirstName="ادمین",
                    LastName="ادمین",
                    UserTypeId = (int)UserTypeEnum.Users_With_In_The_Organization,
                    Username="admin",
                    UserGuid = Guid.NewGuid(),
                    NationalCode="0017300762",
                }
            };
        }
        public override string ModelName => "User";

        public override string Schema => "Organization";

        public override List<User> Items { get; set; }
    }
}
