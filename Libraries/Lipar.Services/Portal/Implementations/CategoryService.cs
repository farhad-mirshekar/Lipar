using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.Portal;
using Lipar.Services.Portal.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Portal.Implementations
{
    public class CategoryService : ICategoryService
    {
        #region Fileds
        private readonly IRepository<Category> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public CategoryService(IRepository<Category> repository
                             , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public void Add(Category model)
        {
            model.UserId = _workContext.CurrentUser.Id;

            _repository.Insert(model);
        }

        public void Delete(Category model)
        {
            model.RemoveDate = DateTime.Now;
            model.RemoverId = _workContext.CurrentUser.Id;

            if (model.Children != null && model.Children.Count > 0)
            {
                foreach (var child in model.Children)
                {
                    child.RemoveDate = model.RemoveDate;
                    child.RemoverId = _workContext.CurrentUser.Id;
                }
            }

            _repository.Update(model);
        }

        public void Edit(Category model)
        {
            _repository.Update(model);
        }

        public Category GetById(int Id)
        {
            var category =  _repository.GetById(Id);

            if (category.RemoverId.HasValue && category.RemoverId.Value != 0)
                return null;

            return category;
        }

        public IPagedList<Category> List(CategoryListVM listVM)
        {
            var query = _repository.Table;

            query = query.Where(c => c.RemoverId == null);

            var categories = new PagedList<Category>(query, listVM.PageIndex, listVM.PageSize);

            return categories;
        }

        public string GetFormattedBreadCrumb(Category category, string separator = ">>")
        {
             category = GetById(category.Id);

            string result = string.Empty;

            var alreadyProcessedCategoryIds = new List<int>() { };

            while (category != null && category.Id != 0 &&  //not null
                 !alreadyProcessedCategoryIds.Contains(category.Id)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", category.Name, separator, result);
                }

                alreadyProcessedCategoryIds.Add(category.Id);

                if (category.ParentId.HasValue && category.ParentId.Value != 0)
                {
                    category = GetById(category.ParentId.Value);
                }

            }
            return result;
        }
        #endregion
    }
}
