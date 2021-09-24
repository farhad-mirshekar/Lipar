using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    public interface IMenuService
    {
        void Add(Menu model);
        void Edit(Menu model);
        Menu GetById(int Id);
        IPagedList<Menu> List(MenuListVM listVM);
        void Delete(Menu model);
    }
}
