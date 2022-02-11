using Lipar.Core;
using Lipar.Entities.Domain.Application;
using System;

namespace Lipar.Services.Application.Contracts
{
   public interface IProductQuestionService
    {
        /// <summary>
        /// add product question method
        /// </summary>
        /// <param name="model">product question</param>
        void Add(ProductQuestion model);
        
        /// <summary>
        /// edit product question method
        /// </summary>
        /// <param name="model">product question</param>
        void Edit(ProductQuestion model);

        /// <summary>
        /// delete product question method
        /// </summary>
        /// <param name="model">product question</param>
        void Delete(ProductQuestion model);

        /// <summary>
        /// <summary>
        ///  retrieve product question by id method
        /// </summary>
        /// <param name="Id">id</param>
        /// <param name="noTracking">if param True, model retrieve no tracking</param>
        /// <returns></returns>
        ProductQuestion GetById(Guid Id, bool noTracking = false);

        /// <summary>
        /// list product question method
        /// </summary>
        /// <param name="listVM">product question list vm</param>
        /// <returns></returns>
        IPagedList<ProductQuestion> List(ProductQuestionListVM listVM);
    }
}
