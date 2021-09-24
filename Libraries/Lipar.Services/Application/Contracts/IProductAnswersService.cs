using Lipar.Core;
using Lipar.Entities.Domain.Application;

namespace Lipar.Services.Application.Contracts
{
   public interface IProductAnswersService
    {
        /// <summary>
        /// add product answers method
        /// </summary>
        /// <param name="model">product answers</param>
        void Add(ProductAnswers model);

        /// <summary>
        /// edit product answers method
        /// </summary>
        /// <param name="model">product answers</param>
        void Edit(ProductAnswers model);

        /// <summary>
        /// delete product answers method
        /// </summary>
        /// <param name="model">product answers</param>
        void Delete(ProductAnswers model);

        /// <summary>
        /// <summary>
        ///  retrieve product answers by id method
        /// </summary>
        /// <param name="Id">id</param>
        /// <param name="noTracking">if param True, model retrieve no tracking</param>
        /// <returns></returns>
        ProductAnswers GetById(int Id, bool noTracking = false);

        /// <summary>
        /// list product answers method
        /// </summary>
        /// <param name="listVM">product answers list vm</param>
        /// <returns></returns>
        IPagedList<ProductAnswers> List(ProductAnswersListVM listVM);
    }
}
