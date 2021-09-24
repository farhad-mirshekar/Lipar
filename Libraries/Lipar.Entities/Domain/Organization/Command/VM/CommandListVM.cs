namespace Lipar.Entities.Domain.Organization
{
    public class CommandListVM : BaseListVM
    {
        public int? RoleId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
