using Lipar.Core;
using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Services.General.Contracts
{
    public interface IMenuItemService
    {
        void Add(MenuItem model);
        void Edit(MenuItem model);
        MenuItem GetById(Guid Id);
        IPagedList<MenuItem> List(MenuItemListVM listVM);
        void Delete(MenuItem model);
    }
}
