using System;

namespace Lipar.Entities.Domain.General
{
    public class MenuItemListVM : BaseListVM
    {
        public string Name { get; set; }
        public Guid MenuId { get; set; }
    }
}
