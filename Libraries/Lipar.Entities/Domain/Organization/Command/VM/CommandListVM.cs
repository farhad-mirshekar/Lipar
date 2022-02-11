using System;

namespace Lipar.Entities.Domain.Organization
{
    public class CommandListVM : BaseListVM
    {
        public Guid? RoleId { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
    }
}
