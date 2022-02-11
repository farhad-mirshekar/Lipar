using System;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class ActivityLogModel : BaseEntityModel<Guid>
    {
        #region Fields
        public string Comment { get; set; }
        public string ActivityLogType { get; set; }
        #endregion
    }
}
