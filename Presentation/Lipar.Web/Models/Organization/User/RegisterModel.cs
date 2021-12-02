using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Models.Organization
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string CellPhone { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        [Remote(action:"CheckDuplicateUserName",controller:"Account")]
        public string UserName { get; set; }
        public string Captcha { get; set; }
    }
}
