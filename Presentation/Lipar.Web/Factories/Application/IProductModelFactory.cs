using Lipar.Entities.Domain.Application;
using Lipar.Web.Models.Application;
using System.Collections.Generic;

namespace Lipar.Web.Factories.Application
{
   public interface IProductModelFactory
    {
        /// <summary>
        /// latest products list
        /// </summary>
        /// <param name="PageIndex">page index</param>
        /// <param name="PageSize">page size</param>
        /// <returns></returns>
        ProductListModel LatestProducts(int PageIndex = 0, int PageSize = 5);
        /// <summary>
        /// prepare product model
        /// </summary>
        /// <param name="model">product model</param>
        /// <param name="product">product</param>
        /// <param name="showComment">show comment</param>
        /// <returns></returns>
        ProductModel PrepareProductModel(ProductModel model , Product product , bool showComment);
        /// <summary>
        /// prepare product comment list model
        /// </summary>
        /// <param name="productComments">product comments</param>
        /// <returns></returns>
        IList<ProductCommentListModel> PrepareProductCommentListModel(IList<ProductComment> productComments);

        /// <summary>
        /// prepare product question list model
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        IList<ProductQuestionModel> PrepareProductQuestionListModel(Product product);
        /// <summary>
        /// prepare product question model
        /// </summary>
        /// <param name="model">product question model</param>
        /// <param name="product">product</param>
        /// <returns></returns>
        ProductQuestionModel PrepareProductQuestionModel(ProductQuestionModel model,Product product);
    }
}
