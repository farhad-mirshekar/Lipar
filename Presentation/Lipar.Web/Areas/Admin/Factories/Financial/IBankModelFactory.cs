using Lipar.Entities.Domain.Financial;
using Lipar.Web.Areas.Admin.Models.Financial;

namespace Lipar.Web.Areas.Admin.Factories.Financial
{
   public interface IBankModelFactory
    {
        /// <summary>
        /// prepare bank list model
        /// </summary>
        /// <param name="searchModel">bank search model</param>
        /// <returns></returns>
        BankListModel PrepareBankListModel(BankSearchModel searchModel);

        /// <summary>
        /// prepare bank model
        /// </summary>
        /// <param name="model">bank model</param>
        /// <param name="bank">bank</param>
        /// <returns></returns>
        BankModel PrepareBankModel(BankModel model, Bank bank);

        /// <summary>
        /// prepare bank port list model
        /// </summary>
        /// <param name="bankPortSearchModel">bank port list model</param>
        /// <returns></returns>
        BankPortListModel PrepareBankPortListModel(BankPortSearchModel bankPortSearchModel);

        /// <summary>
        /// prepare bank port model
        /// </summary>
        /// <param name="model">bank port model</param>
        /// <param name="bankPort">bank port</param>
        /// <returns></returns>
        BankPortModel PrepareBankPortModel(BankPortModel model, BankPort bankPort);
    }
}
