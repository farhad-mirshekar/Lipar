using Lipar.Entities.Domain.Core;

namespace Lipar.Entities.Domain.General
{
    public class ActivityLogType : BaseEntity
    {
        public string SystemKeyword { get; set; }
        public string Name { get; set; }
        public int ViewStatusId { get; set; }

        #region Navigations
        public ViewStatus ViewStatus { get; set; }
        #endregion
    }
}
