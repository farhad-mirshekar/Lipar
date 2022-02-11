using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class MenuService : IMenuService
    {
        #region Fields
        private readonly IRepository<Menu> _repository;
        #endregion

        #region Ctor
        public MenuService(IRepository<Menu> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(Menu model)
        {
             _repository.Insert(model);
        }

        public void Delete(Menu model)
        => _repository.Delete(model);

        public void Edit(Menu model)
        {
             _repository.Update(model);
        }

        public Menu GetById(Guid Id)
        {
            var menu = _repository.GetById(Id);

            return menu;
        }
        public IPagedList<Menu> List(MenuListVM listVM)
        {
            var query = _repository.Table;

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(l => l.Name.Contains(listVM.Name.Trim()));

            var Menus = new PagedList<Menu>(query, listVM.PageIndex, listVM.PageSize, false);

            return Menus;
        }
        #endregion
    }
}
