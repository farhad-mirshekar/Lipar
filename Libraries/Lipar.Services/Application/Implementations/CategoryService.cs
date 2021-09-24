using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class CategoryService : ICategoryService
    {
        #region Ctor
        public CategoryService(IRepository<Category> repository
                             , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Fields
        private readonly IRepository<Category> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Methods
        public void Add(Category model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.UserId = _workContext.CurrentUser.Id;
            _repository.Insert(model);
        }

        public void Delete(Category model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.RemoverId = _workContext.CurrentUser.Id;
            model.RemoveDate = CommonHelper.GetCurrentDateTime();

            if(model.Children != null && model.Children.Count > 0)
            {
                foreach (var category in model.Children)
                {
                    category.ParentId = null;
                }
            }

            Edit(model);
        }

        public void Edit(Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.UserId = _workContext.CurrentUser.Id;
            _repository.Update(model);
        }

        public Category GetById(int Id)
        {
            if(Id == 0)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            return _repository.GetById(Id);
        }

        public IPagedList<Category> List(CategoryListVM listVM)
        {
            var query = _repository.TableNoTracking;

            if (!string.IsNullOrEmpty(listVM.Name))
            {
                query = query.Where(c => c.Name.Contains(listVM.Name.Trim()));
            }

            var models = new PagedList<Category>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        public string GetFormattedBreadCrumb(Category category, string separator = ">>")
        {
            category = GetById(category.Id);
            if(category == null)
            {
                return string.Empty;
            }

            var breadCrumb = string.Empty;
            var alreadyProcessedCategoryIds = new List<int>();

            while(category != null && category.Id != 0 &&
                !alreadyProcessedCategoryIds.Contains(category.Id))
            {
                if (string.IsNullOrEmpty(breadCrumb))
                {
                    breadCrumb = category.Name;
                }
                else
                {
                    breadCrumb = $"{category.Name} {separator} {breadCrumb}";
                }

                alreadyProcessedCategoryIds.Add(category.Id);

                if(category.ParentId.HasValue && category.ParentId.Value != 0)
                {
                    category = GetById(category.ParentId.Value);
                }
            }

            return breadCrumb;
        }
        #endregion
    }
}
