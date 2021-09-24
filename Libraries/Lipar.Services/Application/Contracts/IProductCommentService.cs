using Lipar.Core;
using Lipar.Entities.Domain.Application;

namespace Lipar.Services.Application.Contracts
{
   public interface IProductCommentService
    {
        /// <summary>
        /// product comment add method
        /// </summary>
        /// <param name="model">product comment</param>
        void Add(ProductComment model);
        
        /// <summary>
        /// product comment edit method
        /// </summary>
        /// <param name="model">product comment</param>
        void Edit(ProductComment model);

        /// <summary>
        /// product comment delete method
        /// </summary>
        /// <param name="model">product comment</param>
        void Delete(ProductComment model);

        /// <summary>
        ///  retrieve product comment by id method
        /// </summary>
        /// <param name="Id">id</param>
        /// <param name="noTracking">if param True, model retrieve no tracking</param>
        /// <returns></returns>
        ProductComment GetById(int Id , bool noTracking = false);

        /// <summary>
        /// product comment list method
        /// </summary>
        /// <param name="listVM">product comment list vm</param>
        /// <returns></returns>
        IPagedList<ProductComment> List(ProductCommentListVM listVM);

    }
}
