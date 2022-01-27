using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    /// <summary>
    /// email account service
    /// </summary>
   public interface IEmailAccountService
    {
        /// <summary>
        /// add email account method
        /// </summary>
        /// <param name="model">email account</param>
        void Add(EmailAccount model);

        /// <summary>
        /// edit email account method
        /// </summary>
        /// <param name="model">email account</param>
        void Edit(EmailAccount model);

        /// <summary>
        /// retrieve email account by id method
        /// </summary>
        /// <param name="id">email account id</param>
        /// <param name="noTracking">if param True, model retrieve no tracking</param>
        /// <returns></returns>
        EmailAccount GetById(int id, bool noTracking = false);

        /// <summary>
        /// list email account method
        /// </summary>
        /// <param name="listVM">email account list view model</param>
        /// <returns></returns>
        IPagedList<EmailAccount> List(EmailAccountListVM listVM);
    }
}
