using Lipar.Web.Framework.Models;

namespace Lipar.Web.Areas.Admin.Models.Financial
{
    public class BankPortSearchModel : BaseSearchModel
    {
        public BankPortSearchModel()
        {
            AddBankPortModel = new BankPortModel();
        }
        public int BankId { get; set; }

        public BankPortModel AddBankPortModel { get; set; }
    }
}
