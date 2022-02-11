using Lipar.Core;
using Lipar.Entities.Domain.Financial;
using System;
using System.Linq;

namespace Lipar.Services.Financial.Contracts
{
    /// <summary>
    /// bank port service
    /// </summary>
   public interface IBankPortService
    {
        /// <summary>
        /// add bank port method
        /// </summary>
        /// <param name="model">bank port</param>
        void Add(BankPort model);

        /// <summary>
        /// edit bank port method
        /// </summary>
        /// <param name="model">bank port</param>
        void Edit(BankPort model);

        /// <summary>
        /// delete bank port method
        /// </summary>
        /// <param name="model">bank port</param>
        void Delete(BankPort model);

        /// <summary>
        /// retrieve bank port by id method
        /// </summary>
        /// <param name="id">bank port id</param>
        /// <param name="noTracking">if param True, model retrieve no tracking</param>
        /// <returns></returns>
        BankPort GetById(Guid id, bool noTracking = false);

        /// <summary>
        /// list bank port method
        /// </summary>
        /// <param name="listVM">bank port list view model</param>
        /// <returns></returns>
        IPagedList<BankPort> List(BankPortListVM listVM);

        /// <summary>
        /// query bankPort
        /// </summary>
        /// <returns></returns>
        IQueryable<BankPort> GetBankPorts();
    }
}
