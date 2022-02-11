using System;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class EmailAccountModel : BaseEntityModel<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
