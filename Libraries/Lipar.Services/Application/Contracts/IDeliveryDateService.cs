using Lipar.Core;
using Lipar.Entities.Domain.Application;

namespace Lipar.Services.Application.Contracts
{
   public interface IDeliveryDateService
    {

        /// <summary>
        /// add delivery date product method
        /// </summary>
        /// <param name="model"></param>
        void Add(DeliveryDate model);
        /// <summary>
        /// edit delivery date product method
        /// </summary>
        /// <param name="model"></param>
        void Edit(DeliveryDate model);
        /// <summary>
        /// delete delivery date product method
        /// </summary>
        /// <param name="model"></param>
        void Delete(DeliveryDate model);
        /// <summary>
        /// retrieve delivery date product by id method
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        DeliveryDate GetById(int Id);
        /// <summary>
        /// list delivery date product method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<DeliveryDate> List(DeliveryDateListVM listVM);
    }
}
