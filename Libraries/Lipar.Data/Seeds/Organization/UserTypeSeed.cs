using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.Organization
{
    public class UserTypeSeed : BaseSeed<UserType>
    {
        public UserTypeSeed()
        {
            Items = new List<UserType>()
            {
                new UserType { Id = 1, Title="کاربر درون سازمان"},
                new UserType { Id = 2, Title = "کاربر بیرون سازمان"}
            };
        }
        public override string ModelName => "UserType";

        public override string Schema => "Organization";

        public override List<UserType> Items { get; set; }
    }
}
