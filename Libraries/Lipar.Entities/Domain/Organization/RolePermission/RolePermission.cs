namespace Lipar.Entities.Domain.Organization
{
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int CommandId { get; set; }
        public Command Command { get; set; }
    }
}
