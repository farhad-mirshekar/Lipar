using Lipar.Core;
using Lipar.Entities.Domain.Application;
using System;

namespace Lipar.Services.Application.Contracts
{
   public interface IProductAttributeService
    {
        /// <summary>
        /// add attribute product method
        /// </summary>
        /// <param name="model"></param>
        void Add(ProductAttribute model);
        /// <summary>
        /// edit attribute product method
        /// </summary>
        /// <param name="model"></param>
        void Edit(ProductAttribute model);
        /// <summary>
        /// delete attribute product method
        /// </summary>
        /// <param name="model"></param>
        void Delete(ProductAttribute model);
        /// <summary>
        /// retrieve attribute product by id method
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProductAttribute GetById(Guid Id);
        /// <summary>
        /// list attribute product method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<ProductAttribute> List(ProductAttributeListVM listVM);
    }
}
