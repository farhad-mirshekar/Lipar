using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Financial
{
    public class BankPortSearchModel : BaseSearchModel
    {
        public BankPortSearchModel()
        {
            AddBankPortModel = new BankPortModel();
        }
        public Guid BankId { get; set; }

        public BankPortModel AddBankPortModel { get; set; }
    }
}
