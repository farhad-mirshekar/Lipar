using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Portal
{
    public class CategoryModelFactory : ICategoryModelFactory
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Ctor
        public CategoryModelFactory(ICategoryService categoryService
                                  , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _categoryService = categoryService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }

        #endregion
        public CategoryListModel PrepareCategoryListModel(CategorySearchModel searchModel)
        {
            var categories = _categoryService.List(new CategoryListVM { PageIndex = searchModel.Page - 1, PageSize = searchModel.PageSize, Name = searchModel.Name });

            var model = new CategoryListModel().PrepareToGrid(searchModel, categories, () =>
           {
               return categories.Select(category =>
               {
                   var categoryModel = category.ToModel<CategoryModel>();
                   categoryModel.NameCrumb = _categoryService.GetFormattedBreadCrumb(category) ?? "";
                   return categoryModel;
               });
           });

            return model;
        }

        public CategoryModel PrepareCategoryModel(CategoryModel model, Category category)
        {
            if (category != null) // for edit
                model ??= category.ToModel<CategoryModel>();

            model.AvailableCategories = _baseAdminModelFactory.PrepareCategoriesForPortal();

            return model;
        }
    }
}
