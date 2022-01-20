using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Financial
{
    public class BankSearchModel : BaseSearchModel
    {
        public BankSearchModel()
        {
            SetPopupGridPageSize();
        }
        public string Name { get; set; }
    }
}
