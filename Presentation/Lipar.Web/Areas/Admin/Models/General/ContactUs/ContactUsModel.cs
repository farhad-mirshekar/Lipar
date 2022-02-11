using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class ContactUsModel : BaseEntityModel<Guid>
    {
        #region Fields
        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// پست الکترونیک
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// موضوع
        /// </summary>
        public int ContactUsTypeId { get; set; }
        /// <summary>
        /// متن اصلی
        /// </summary>
        public string Body { get; set; }
        #endregion

        #region Navigations
        public ContactUsType ContactUsType { get; set; }
        #endregion
    }
}
