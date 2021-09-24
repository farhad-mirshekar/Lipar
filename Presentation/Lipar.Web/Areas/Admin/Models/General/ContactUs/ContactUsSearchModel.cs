using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.General
{
    /// <summary>
    /// contact us search
    /// </summary>
    public class ContactUsSearchModel : BaseSearchModel
    {
        #region Ctor
        public ContactUsSearchModel()
        {
            AvailableContactUsType = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public int ContactUsTypeId { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableContactUsType { get; set; }
        #endregion
    }
}
