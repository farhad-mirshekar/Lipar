using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public class CategoryModelFactory : ICategoryModelFactory
    {
        #region Ctor
        public CategoryModelFactory(ICategoryService categoryService
                                  , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _categoryService = categoryService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Methods
        public CategoryListModel PrepareCategoryListModel(CategorySearchModel searchModel)
        {
            var categories = _categoryService.List(new CategoryListVM
            {
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
                Name = searchModel.Name,
            });

            if(categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            var models = new CategoryListModel().PrepareToGrid(searchModel, categories, () =>
            {
                return categories.Select(category =>
                {
                    var categoryModel = category.ToModel<CategoryModel>();

                    categoryModel.NameCrumb = _categoryService.GetFormattedBreadCrumb(category);

                    return categoryModel;
                });
            });

            return models;
        }

        public CategoryModel PrepareCategoryModel(CategoryModel model, Category category)
        {
            if(category != null)
            {
                model = category.ToModel<CategoryModel>();
            }

            //fill all category for drop
            model.AvailableCategories = _baseAdminModelFactory.PrepareCategoriesForProduct();

            //fill enable type for drop
            _baseAdminModelFactory.PrepareEnabledType(model.AvailableEnableType);

            return model;
        }
        #endregion
    }
}
