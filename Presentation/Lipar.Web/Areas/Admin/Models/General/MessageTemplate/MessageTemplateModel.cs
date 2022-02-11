using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class MessageTemplateModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public MessageTemplateModel()
        {
            AvailableEmailAccounts = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
        public bool IsActive { get; set; }
        public int EmailAccountId { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableEmailAccounts { get; set; }
        #endregion
    }
}
