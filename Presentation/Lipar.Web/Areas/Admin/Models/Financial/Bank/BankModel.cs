using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Financial
{
    public class BankModel : BaseEntityModel
    {
        public BankModel()
        {
            AvailableEnableTypes = new List<SelectListItem>();
            BankPortSearchModel = new BankPortSearchModel();
        }

        #region Fields
        public string Name { get; set; }
        public string PaymentUri { get; set; }
        public string ServiceUri { get; set; }
        public string RedirectUri { get; set; }
        public int? TransactionCost { get; set; }
        public int EnabledTypeId { get; set; }
        public string EnableTypeTitle { get; set; }
        public int UserId { get; set; }
        public BankPortSearchModel BankPortSearchModel { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableEnableTypes { get; set; }
        #endregion
    }
}
