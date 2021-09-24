using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public interface IProductQuestionModelFactory
    {
        /// <summary>
        /// prepare product question list model
        /// </summary>
        /// <param name="searchModel">product question search model</param>
        /// <returns></returns>
        ProductQuestionListModel PrepareProductQuestionListModel(ProductQuestionSearchModel searchModel);

        /// <summary>
        /// prepare product question model
        /// </summary>
        /// <param name="model">product question model</param>
        /// <param name="productQuestion">product question</param>
        /// <returns></returns>
        ProductQuestionModel PrepareProductQuestionModel(ProductQuestionModel model, ProductQuestion productQuestion);
        
        /// <summary>
        ///prepare product question search model 
        /// </summary>
        /// <param name="searchModel">product question search model</param>
        /// <param name="product">product</param>
        /// <returns></returns>
        ProductQuestionSearchModel PrepareProductQuestionSearchModel(ProductQuestionSearchModel searchModel, Product product);

        /// <summary>
        /// prepare product answer list model
        /// </summary>
        /// <param name="searchModel">product answer search model</param>
        /// <returns></returns>
        ProductAnswersListModel PrepareProductAnswersListModel(ProductAnswersSearchModel searchModel);

        /// <summary>
        /// prepare product answer model
        /// </summary>
        /// <param name="model">product answer model</param>
        /// <param name="productAnswers">product answer</param>
        /// <returns></returns>
        ProductAnswersModel PrepareProductAnswersModel(ProductAnswersModel model, ProductAnswers productAnswers);

        /// <summary>
        /// prepare product answer search model
        /// </summary>
        /// <param name="searchModel">product answer search model</param>
        /// <param name="productQuestion">product question</param>
        /// <returns></returns>
        ProductAnswersSearchModel PrepareProductAnswersSearchModel(ProductAnswersSearchModel searchModel, ProductQuestion productQuestion);
    }
}
