namespace Lipar.Entities.Domain.Organization
{
    public class PositionListVM : BaseListVM
    {
        #region Ctor
        public PositionListVM()
        {
            UserListVM = new UserListVM();
        }
        #endregion

        public int? UserId { get; set; }
        public int? DepartmentId { get; set; }
        public int? EnabledTypeId { get; set; }
        public UserListVM UserListVM { get; set; }
    }
}
