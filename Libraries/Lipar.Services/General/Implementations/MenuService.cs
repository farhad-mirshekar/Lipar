using Lipar.Core;
using Lipar.Data;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class MenuService : IMenuService
    {
        #region Fields
        private readonly IRepository<Menu> _repository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public MenuService(IRepository<Menu> repository
                         , IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public void Add(Menu model)
        {
            model.UserId = _workContext.CurrentUser.Id;
             _repository.Insert(model);
        }

        public void Delete(Menu model)
        => _repository.Delete(model);

        public void Edit(Menu model)
        {
            model.UserId = _workContext.CurrentUser.Id;
             _repository.Update(model);
        }

        public Menu GetById(int Id)
        {
            var menu = _repository.GetById(Id);

            if (menu.RemoverId.HasValue && menu.RemoverId.Value != 0)
                return null;

            return menu;
        }
        public IPagedList<Menu> List(MenuListVM listVM)
        {
            var query = _repository.Table;
            query = query.Where(m => m.RemoverId != 0);

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(l => l.Name.Contains(listVM.Name.Trim()));

            var Menus = new PagedList<Menu>(query, listVM.PageIndex, listVM.PageSize, false);

            return Menus;
        }
        #endregion
    }
}
