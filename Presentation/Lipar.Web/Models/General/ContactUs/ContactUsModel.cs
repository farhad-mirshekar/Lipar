using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.General
{
    public class ContactUsModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public ContactUsModel()
        {
            AvailableContactUsType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int ContactUsTypeId { get; set; }
        public string Body { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableContactUsType { get; set; }
        #endregion

        #region Utilities
        /// <summary>
        /// نتیجه عملیات
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// متن پیغام
        /// </summary>
        public string Result { get; set; }
        #endregion
    }
}
