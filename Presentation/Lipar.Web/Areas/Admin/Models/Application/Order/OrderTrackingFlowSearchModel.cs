using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class OrderTrackingFlowSearchModel : BaseSearchModel
    {
        public Guid ToPositionId { get; set; }
        public Guid? OrderTrackingId { get; set; }
    }
}
