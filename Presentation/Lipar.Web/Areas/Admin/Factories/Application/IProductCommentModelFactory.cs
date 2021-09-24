using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public interface IProductCommentModelFactory
    {
        /// <summary>
        /// prepare product comment list model
        /// </summary>
        /// <param name="searchModel">product comment search model</param>
        /// <returns></returns>
        ProductCommentListModel PrepareProductCommentListModel(ProductCommentSearchModel searchModel);
        
        /// <summary>
        /// prepare product comment search model
        /// </summary>
        /// <returns></returns>
        ProductCommentSearchModel PrepareProductCommentSearchModel();

        /// <summary>
        /// prepare product comment model
        /// </summary>
        /// <param name="model">product comment model</param>
        /// <param name="productComment">product comment</param>
        /// <returns></returns>
        ProductCommentModel PrepareProductCommentModel(ProductCommentModel model, ProductComment productComment);
    }
}
