using Lipar.Core;
using Lipar.Entities.Domain.Application;

namespace Lipar.Services.Application.Contracts
{
   public interface IShippingCostService
    {

        /// <summary>
        /// add shipping cost product method
        /// </summary>
        /// <param name="model"></param>
        void Add(ShippingCost model);
        /// <summary>
        /// edit shipping cost product method
        /// </summary>
        /// <param name="model"></param>
        void Edit(ShippingCost model);
        /// <summary>
        /// delete shipping cost product method
        /// </summary>
        /// <param name="model"></param>
        void Delete(ShippingCost model);
        /// <summary>
        /// retrieve shipping cost product by id method
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ShippingCost GetById(int Id);
        /// <summary>
        /// list shipping cost product method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<ShippingCost> List(ShippingCostListVM listVM);
    }
}
