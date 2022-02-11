using System;

namespace Lipar.Entities.Domain.Organization
{
    public class UserPassword : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public int PasswordFormatTypeId { get; set; }
        public string PasswordSalt { get; set; }

        #region Navigations
        public PasswordFormatType PasswordFormatType { get; set; }
        public User User { get; set; }
        #endregion
    }
}
