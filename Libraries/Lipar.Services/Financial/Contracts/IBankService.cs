using Lipar.Core;
using Lipar.Entities.Domain.Financial;
using System;

namespace Lipar.Services.Financial.Contracts
{
    /// <summary>
    /// bank service
    /// </summary>
   public interface IBankService
    {
        /// <summary>
        /// add bank method
        /// </summary>
        /// <param name="model">bank</param>
        void Add(Bank model);

        /// <summary>
        /// edit bank method
        /// </summary>
        /// <param name="model">bank</param>
        void Edit(Bank model);

        /// <summary>
        /// delete bank method
        /// </summary>
        /// <param name="model">bank</param>
        void Delete(Bank model);

        /// <summary>
        /// retrieve bank by id method
        /// </summary>
        /// <param name="id">bank id</param>
        /// <param name="noTracking">if param True, model retrieve no tracking</param>
        /// <returns></returns>
        Bank GetById(Guid id, bool noTracking = false);

        /// <summary>
        /// list bank method
        /// </summary>
        /// <param name="listVM">bank list view model</param>
        /// <returns></returns>
        IPagedList<Bank> List(BankListVM listVM);
    }
}
