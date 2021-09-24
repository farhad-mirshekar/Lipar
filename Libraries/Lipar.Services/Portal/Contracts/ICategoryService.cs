using Lipar.Core;
using Lipar.Entities.Domain.Portal;

namespace Lipar.Services.Portal.Contracts
{
    public interface ICategoryService
    {
        void Add(Category model);
        void Edit(Category model);
        Category GetById(int Id);
        void Delete(Category model);
        IPagedList<Category> List(CategoryListVM listVM);
        string GetFormattedBreadCrumb(Category category, string separator = ">>");
    }
}
