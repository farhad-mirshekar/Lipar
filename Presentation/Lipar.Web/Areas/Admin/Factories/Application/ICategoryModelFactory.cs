using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
   public interface ICategoryModelFactory
    {
        /// <summary>
        /// prepare category list model
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        CategoryListModel PrepareCategoryListModel(CategorySearchModel searchModel);
        /// <summary>
        /// prepare category model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        CategoryModel PrepareCategoryModel(CategoryModel model, Category category);
    }
}
