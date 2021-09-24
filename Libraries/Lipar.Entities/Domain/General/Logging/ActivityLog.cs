using Lipar.Entities.Domain.Organization;

namespace Lipar.Entities.Domain.General
{
    public class ActivityLog : BaseEntity
    {
        #region Fields
        public int? UserId { get; set; }
        public string Comment { get; set; }
        public int ActivityLogTypeId { get; set; }
        public string IpAddress { get; set; }
        public int? EntityId { get; set; }
        public string EntityName { get; set; }
        #endregion

        #region Navigations
        public ActivityLogType ActivityLogType { get; set; }
        public User User { get; set; }
        #endregion
    }
}
