using Lipar.Entities.Domain.Portal;
using Lipar.Web.Areas.Admin.Models.Portal;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public interface ICategoryModelFactory
    {
        CategoryListModel PrepareCategoryListModel(CategorySearchModel model);
        CategoryModel PrepareCategoryModel(CategoryModel model, Category category);
    }
}
