using Lipar.Core;
using Lipar.Entities.Domain.Application;
using System;

namespace Lipar.Services.Application.Contracts
{
   public interface ICategoryService
    {
        /// <summary>
        /// add category product method
        /// </summary>
        /// <param name="model"></param>
        void Add(Category model);
        /// <summary>
        /// edit category product method
        /// </summary>
        /// <param name="model"></param>
        void Edit(Category model);
        /// <summary>
        /// delete category product method
        /// </summary>
        /// <param name="model"></param>
        void Delete(Category model);
        /// <summary>
        /// retrieve category product by id method
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Category GetById(Guid Id);
        /// <summary>
        /// list category product method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<Category> List(CategoryListVM listVM);
        /// <summary>
        /// get bread crumb 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        string GetFormattedBreadCrumb(Category category, string separator = ">>");
    }
}
