namespace Lipar.Entities.Domain.Organization
{
    public class PositionRole : BaseEntity
    {
        public Position Position { get; set; }
        public int PositionId { get; set; }

        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
