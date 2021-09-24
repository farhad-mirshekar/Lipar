namespace Lipar.Entities.Domain.Organization
{
    public class RoleListVM : BaseListVM
    {
        public int? CenterId { get; set; }
        public int? PositionId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
    }
}
