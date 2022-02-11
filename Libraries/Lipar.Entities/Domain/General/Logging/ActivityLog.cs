using System;

namespace Lipar.Entities.Domain.General
{
    public class ActivityLog : BaseEntity<Guid>
    {
        #region Fields
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int ActivityLogTypeId { get; set; }
        public string IpAddress { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        #endregion

        #region Navigations
        public ActivityLogType ActivityLogType { get; set; }
        #endregion
    }
}
