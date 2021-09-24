﻿using Lipar.Core;
using Lipar.Entities.Domain.Application;

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
        /// <returns></returns>
        ProductAttributeValue GetById(int Id);
        /// <summary>
        /// list product attribute value method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<ProductAttributeValue> List(ProductAttributeValueListVM listVM);
    }
}
