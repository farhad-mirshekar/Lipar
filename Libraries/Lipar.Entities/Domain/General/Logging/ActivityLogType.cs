using Lipar.Entities.Domain.Core;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.General
{
    public class ActivityLogType : BaseEntity<int>
    {
        public ActivityLogType()
        {
            ActivityLogs = new HashSet<ActivityLog>();
        }
        public string SystemKeyword { get; set; }
        public string Name { get; set; }

        #region Navigations
        public ICollection<ActivityLog> ActivityLogs { get; set; }
        #endregion
    }
}
