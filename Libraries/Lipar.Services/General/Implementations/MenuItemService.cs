using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class MenuItemService : IMenuItemService
    {
        #region Fields
        private readonly IRepository<MenuItem> _repository;
        #endregion

        #region Ctor
        public MenuItemService(IRepository<MenuItem> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(MenuItem model)
        {
            _repository.Insert(model);
        }

        public void Delete(MenuItem model)
        => _repository.Delete(model);

        public void Edit(MenuItem model)
        {
            _repository.Update(model);
        }

        public MenuItem GetById(int Id)
        => _repository.GetById(Id);

        public IPagedList<MenuItem> List(MenuItemListVM listVM)
        {
            var query = _repository.Table;

            query = query.Where(m => m.MenuId == listVM.MenuId);

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(m => m.Name.Contains(listVM.Name));

            var menuItems = new PagedList<MenuItem>(query, listVM.PageIndex, listVM.PageSize, false);

            return menuItems;
        }
        #endregion
    }
}
