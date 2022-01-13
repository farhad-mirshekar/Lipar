using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Financial
{
    public class BankPortModel : BaseEntityModel
    {
        #region Ctor
        public BankPortModel()
        {
            AvailableEnableTypes = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public int BankId { get; set; }
        public string Name { get; set; }
        public string MerchantId { get; set; }
        public string MerchantKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TerminalId { get; set; }
        public bool? IsDefault { get; set; }
        public int EnabledTypeId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string EnableTypeTitle { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableEnableTypes { get; set; }
        #endregion
    }
}
