using Lipar.Core;
using Lipar.Entities.Domain.Application;
using System;

namespace Lipar.Services.Application.Contracts
{
   public interface IProductAttributeValueService
    {
        /// <summary>
        /// add product attribute value method
        /// </summary>
        /// <param name="model"></param>
        void Add(ProductAttributeValue model);
        /// <summary>
        /// edit product attribute value method
        /// </summary>
        /// <param name="model"></param>
        void Edit(ProductAttributeValue model);
        /// <summary>
        /// delete product attribute value method
        /// </summary>
        /// <param name="model"></param>
        void Delete(ProductAttributeValue model);
        /// <summary>
        /// retrieve product attribute value by id method
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="noTracking">if this param True, model retrieve no tracking</param>
        /// <returns></returns>
        ProductAttributeValue GetById(Guid Id,bool noTracking = false);
        /// <summary>
        /// list product attribute value method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<ProductAttributeValue> List(ProductAttributeValueListVM listVM);
    }
}
