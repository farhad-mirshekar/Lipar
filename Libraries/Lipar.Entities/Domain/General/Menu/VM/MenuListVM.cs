using System;

namespace Lipar.Entities.Domain.General
{
    public class MenuListVM : BaseListVM
    {
        public string Name { get; set; }
        public Guid? LanguageId { get; set; }
    }
}
