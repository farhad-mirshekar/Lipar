using System;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class FaqModel : BaseEntityModel<Guid>
    {
        public Guid FaqGroupId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
