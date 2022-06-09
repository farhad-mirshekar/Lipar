using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace Lipar.Web.Models.Organization
{
    public class RegisterModel
    {
        public RegisterModel()
        {
            AvailableGenderList = new List<SelectListItem>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string CellPhone { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        [Remote(action:"CheckDuplicateUserName",controller:"Account")]
        public string UserName { get; set; }
        public string Captcha { get; set; }
        public int GenderId { get; set; }
        public bool ShowCaptcha { get; set; }
        public bool ShowCaptchaInRegisterPage { get; set; }

        public IList<SelectListItem> AvailableGenderList { get; set; }
    }
}
