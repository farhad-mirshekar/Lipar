using System;

namespace Lipar.Web.Areas.Dashboard.Models.Application
{
    public class OrderSearchModel
    {
        public int Page { get; set; }
        public Guid? OrderId { get; set; }
    }
}
