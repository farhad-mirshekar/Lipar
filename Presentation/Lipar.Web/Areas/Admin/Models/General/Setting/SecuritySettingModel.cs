using Lipar.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class SecuritySettingModel : BaseEntityModel, ISettingsModel
    {
        public SecuritySettingModel()
        {
            AvailablePasswordFormatTypes = new List<SelectListItem>();
        }
        /// <summary>
        /// gets or sets the encryption key
        /// </summary>
        public string EncryptionKey { get; set; }
        /// <summary>
        /// gets or sets the password format type
        /// </summary>
        public int PasswordFormatType { get; set; }

        #region Select
        public IList<SelectListItem> AvailablePasswordFormatTypes { get; set; }
        #endregion
    }
}
